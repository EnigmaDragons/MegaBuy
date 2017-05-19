﻿using System;
using MegaBuy.Money.Accounts;
using MegaBuy.Temp;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Money
{
    public class MoneyUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(1600 - Sizes.SmallMargin - Sizes.SmallLabel.Width - Sizes.OverlayIcon.Width - Sizes.Margin, 900 - Sizes.SmallMargin - Sizes.SmallLabel.Height));
        private readonly PlayerAccount _account;
        private readonly Label _label;

        public MoneyUI()
        {
            _account = CurrentGameState.State.PlayerAccount;
            _label = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(Sizes.SmallLabel) };
        }

        public void Update(TimeSpan delta)
        {
            _label.Text = $"$ - {_account.Amount()}";
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label-small", parentTransform + new Transform2(_transform.Location, Sizes.SmallLabel));
            _label.Draw(parentTransform + _transform);
        }
    }
}
