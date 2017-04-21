using System;
using System.Collections.Generic;
using MegaBuy.Apartment.Map;
using MegaBuy.Player.Energy;
using MegaBuy.Player.Hungers;
using MegaBuy.Shopping;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Player
{
    public class PlayerCharacter : IVisualAutomaton
    {
        // Character Systems
        private readonly List<IAutomaton> _components = new List<IAutomaton>();

        // World Location
        private readonly ICharSpace _charSpace;

        // Spatial
        private Transform2 _transform;

        // Motion
        private float moveSpeed = 0.12f;

        // Collision
        private BoxCollider Collider => new BoxCollider(_transform + new Vector2(5, 22), new Size2(22, 10));

        // Animations
        private readonly Animations _anims;

        // Interaction
        private readonly ColoredRectangle _interactRect = new ColoredRectangle {Color = Color.FromNonPremultiplied(255, 0, 0, 100)};
        private TileLocation _interactLocation;

        private bool _isSleeping = false;
        private Direction _dir;
        private string _facing = "Down";
        private bool IsMoving => _dir.HDir != HorizontalDirection.None || _dir.VDir != VerticalDirection.None;
        private string AnimState => $"{_facing}-{IsMoving}";

        public PlayerCharacter(ICharSpace charSpace, Transform2 startingLocation)
        {
            _anims = new PlayerCharacterAnimations();
            _components.Add(new Hunger());
            _components.Add(new PlayerEnergy());
            _transform = startingLocation;
            _charSpace = charSpace;
            Input.ClearBindings();
            Input.OnDirection(UpdatePhysics);
            Input.On(Control.A, Interact);
            World.Subscribe(EventSubscription.Create<WentToBed>((e) => _isSleeping = true, this));
            World.Subscribe(EventSubscription.Create<Awaken>((e) => _isSleeping = false, this));
            World.Subscribe(EventSubscription.Create<CollapsedWithExhaustion>((e) => _isSleeping = true, this));
        }

        private void Interact()
        {
            if(!_isSleeping)
                _charSpace.Interact(_interactLocation);
        }

        private void UpdatePhysics(Direction dir)
        {
            if (!_isSleeping)
            {
                _dir = dir;
                if (!dir.Equals(Direction.None))
                    _transform.Rotation = _dir.ToRotation();

                if (!dir.HDir.Equals(HorizontalDirection.None))
                    _facing = dir.HDir.ToString();
                if (!dir.VDir.Equals(VerticalDirection.None))
                    _facing = dir.VDir.ToString();

                UpdateAnimState();
            }
        }

        public void Update(TimeSpan delta)
        {
            _anims.Update(delta);
            _components.ForEach(x => x.Update(delta));
            var distance = new Physics().GetDistance(moveSpeed, delta);
            if (distance > 0)
                _transform = _charSpace.ApplyMove(_transform, Collider, new Movement(distance, _dir).GetDelta());
            UpdateInteractLocation();
        }

        private void UpdateInteractLocation()
        {
            var playerTile = new TileLocation(_transform);
            var offset = _transform.Rotation.ToDirection().AsOffset();
            _interactLocation = playerTile.Plus(new TileLocation(offset.Y, offset.X));
            _interactRect.Transform = _interactLocation.Transform;
        }

        public void Draw(Transform2 parentTransform)
        {
            var t = _transform + parentTransform;
            _anims.Draw(t);
            //_interactRect.Draw(parentTransform); Uncomment to debug interactions
        }

        private void UpdateAnimState()
        {
            _anims.SetState(AnimState);
        }
    }
}
