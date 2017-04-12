using System;
using MegaBuy.CustomUI;
using MegaBuy.Money;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class MoneyUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(1400 - Sizes.Margin, 850 - Sizes.Margin));
        private readonly PlayerAccount _account;
        private readonly Label _label;

        public MoneyUI()
        {
            _account = GameState.PlayerAccount;
            _label = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(new Size2(200, 50)) };
        }

        public void Update(TimeSpan delta)
        {
            _label.Text = $"MBit - {_account.Amount()}";
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label", parentTransform + new Transform2(_transform.Location, new Size2(200, 50)));
            _label.Draw(parentTransform + _transform);
        }
    }
}
