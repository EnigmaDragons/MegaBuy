using System;
using MegaBuy.Money;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class MoneyUI : IVisualAutomaton
    {
        private readonly Vector2 _location = new Vector2(1400, 850);
        private readonly PlayerAccount _account;
        private readonly Label _label;

        public MoneyUI(PlayerAccount account)
        {
            _account = account;
            _label = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(new Size2(200, 50)) };
        }

        public void Update(TimeSpan delta)
        {
            _label.Text = $"MBit - {_account.Amount()}";
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label", parentTransform + new Transform2(_location, new Size2(200, 50)));
            _label.Draw(parentTransform + new Transform2(_location));
        }
    }
}
