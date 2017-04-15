﻿using System;
using System.Collections.Generic;
using MegaBuy.Apartment.Map;
using MegaBuy.Foods;
using MegaBuy.Player.Energy;
using MegaBuy.Player.Hungers;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

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
        private BoxCollider Collider => 
            new BoxCollider(new Rectangle(_transform.Location.ToPoint(), new Vector2(16 * _transform.Scale, 16 * _transform.Scale).ToPoint()));

        // Animations
        private readonly Animations _anims;

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
            World.Subscribe(EventSubscription.Create<FoodOrdered>(FoodOrdered, this));
        }

        private void FoodOrdered(FoodOrdered order)
        {
            World.Publish(new FoodEaten(order.Food));
        }

        private void Interact()
        {
            var playerTile = new TileLocation(_transform);
            var offset = _transform.Rotation.ToDirection().AsOffset();
            var targetLocation = playerTile.Plus(new TileLocation(offset.Y, offset.X));
            _charSpace.Interact(targetLocation);
        }

        private void UpdatePhysics(Direction dir)
        {
            _dir = dir;
            if (!dir.Equals(Direction.None))
                _transform = _transform + new Transform2(dir.ToRotation());

            if (!dir.HDir.Equals(HorizontalDirection.None))
                _facing = dir.HDir.ToString();
            if (!dir.VDir.Equals(VerticalDirection.None))
                _facing = dir.VDir.ToString();

            UpdateAnimState();
        }

        public void Update(TimeSpan delta)
        {
            _anims.Update(delta);
            _components.ForEach(x => x.Update(delta));
            var distance = new Physics().GetDistance(moveSpeed, delta);
            if (distance > 0)
                _transform = _charSpace.ApplyMove(_transform, Collider, new Movement(distance, _dir).GetDelta());
        }

        public void Draw(Transform2 parentTransform)
        {
            _anims.Draw(_transform + parentTransform);
        }

        private void UpdateAnimState()
        {
            _anims.SetState(AnimState);
        }
    }
}
