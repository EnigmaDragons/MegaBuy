using System;
using MegaBuy.Food;
using MegaBuy.Money;
using MegaBuy.Time;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.CustomUI
{
    public sealed class Overlay : IVisualAutomaton
    {
        private readonly PlayerAccount _account;

        private readonly HungerUI _hungerUi;
        private readonly ClockUI _clockUi;
        private readonly Label _moneyDisplay;

        private readonly Transform2 _transform;

        public Overlay()
            : this(GameState.PlayerAccount, GameState.Clock) { }

        public Overlay(PlayerAccount account, Clock clock)
        {
            _transform = new Transform2(new Vector2(1300, 40));
            _account = account;
            var clockLabel = MakeLabel();
            clockLabel.Transform = clockLabel.Transform + new Vector2(60, 0);
            _clockUi = new ClockUI(clock, clockLabel);
            _hungerUi = new HungerUI(new ImageBox(new Transform2(new Size2(50, 50))));
            _moneyDisplay = MakeLabel();
            _moneyDisplay.Transform = _moneyDisplay.Transform + new Vector2(60, 60);
        }

        public void Update(TimeSpan delta)
        {
            _clockUi.Update(delta);
            _moneyDisplay.Text = $"MBit - {_account.Amount()}";
        }

        public void Draw(Transform2 parentTransform)
        {
            _clockUi.Draw(_transform);
            _hungerUi.Draw(_transform);
            _moneyDisplay.Draw(_transform);
        }

        private static Label MakeLabel()
        {
            return new Label
            {
                Font = "Fonts/Audiowide",
                BackgroundColor = Color.DarkBlue,
                TextColor = Color.LightBlue,
                Transform = new Transform2(new Size2(200, 50))
            };
        }
    }
}
