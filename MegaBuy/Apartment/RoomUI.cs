using System;
using System.Diagnostics;
using MegaBuy.Apartment.Map;
using MegaBuy.Player;
using MegaBuy.Player.Energy;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apartment
{
    public class RoomUI : IVisualAutomatonControl
    {
        private readonly Transform2 _mapTransform = new Transform2(new Vector2(256, 64), Rotation2.None, 2);
        private readonly ApartmentMap _map;
        private readonly NewSelectSleepDurationUI _sleep;
        private readonly GameState _gameState;
        private readonly ColoredRectangle _darkroom = new ColoredRectangle
        {
            Color = Color.FromNonPremultiplied(0, 0, 0, 130),
            Transform = new Transform2(new Size2(1920, 1080))
        };

        private bool _preparingForBed;
        private bool _isSleeping;
        private Transform2 _cameraTransform = Transform2.Zero;
        private float _sleepMillis = 0;

        public ClickUIBranch Branch { get; }

        public RoomUI()
        {
            _gameState = CurrentGameState.State;
            Branch = new ClickUIBranch("Room", (int)ClickUIPriorities.Room);
            _map = ApartmentMapFactory.Create();
            Branch.Add(_map.Branch);
            _sleep = new NewSelectSleepDurationUI(() => { _preparingForBed = false; Branch.Remove(_sleep.Branch); });
            _gameState.PlayerCharacter = new PlayerCharacter(CharacterSex.Male, _map,
                new Transform2(new Vector2(TileSize.Size.Width * 2, TileSize.Size.Height * 3)));

            World.Subscribe(EventSubscription.Create<PreparingForBed>(PrepareForBed, this));
            World.Subscribe(EventSubscription.Create<WentToBed>((e) => WentToBed(), this));
            World.Subscribe(EventSubscription.Create<Awaken>(Awaken, this));
            World.Subscribe(EventSubscription.Create<CollapsedWithExhaustion>((e) => WentToBed(), this));
        }

        private void Awaken(Awaken obj)
        {
            _isSleeping = false;
            _sleepMillis = 0;
        }

        private void WentToBed()
        {
            _preparingForBed = false;
            Branch.Remove(_sleep.Branch);
            _isSleeping = true;
        }

        private void PrepareForBed(PreparingForBed bed)
        {
            _preparingForBed = true;
            Branch.Add(_sleep.Branch);
        }

        public void Update(TimeSpan delta)
        {
            if (_isSleeping)
            {
                _sleepMillis += (float)delta.TotalMilliseconds;
                Debug.WriteLine("Milliseconds Slept: " + _sleepMillis);
                if (_sleepMillis > 500 && _sleepMillis < 5500)
                    _cameraTransform = new Transform2(new Vector2(0, 0.18f * (_sleepMillis - 500)));
                if (_sleepMillis > 5500 && _sleepMillis < 6500) 
                    _cameraTransform = new Transform2(new Vector2(0, 900));
                if (_sleepMillis > 6500 && _sleepMillis < 9500)
                    _cameraTransform = new Transform2(new Vector2(0, 900 - 0.3f * (_sleepMillis - 6500)));
                if (_sleepMillis > 9500)
                    _cameraTransform = Transform2.Zero;
            }
            _map.Update(delta);
            _gameState.PlayerCharacter.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/room-background", new Transform2(new Vector2(0, -900), new Size2(1600, 900)) + _cameraTransform);
            _map.Draw(_mapTransform + _cameraTransform);
            _gameState.PlayerCharacter.Draw(_mapTransform + _cameraTransform);
            if (_preparingForBed)
                _sleep.Draw(Transform2.Zero);
            if (_isSleeping)
                _darkroom.Draw(Transform2.Zero);
        }
    }
}
