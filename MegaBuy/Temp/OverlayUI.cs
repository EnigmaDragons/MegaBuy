﻿using System;
using MegaBuy.CustomUI;
using MegaBuy.Money;
using MegaBuy.Time;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class OverlayUI : IVisualAutomaton
    {
        private readonly Vector2 _location = new Vector2(0, 0);
        private readonly TimeUI _timeUI;
        private readonly TogglePadUI _togglePadUI;
        private readonly MoneyUI _moneyUI;

        public OverlayUI(ClickUIBranch layer, Clock clock, PlayerAccount account)
        {
            _timeUI = new TimeUI(clock);
            _togglePadUI = new TogglePadUI(layer);
            _moneyUI = new MoneyUI(account);
        }

        public void Update(TimeSpan delta)
        {
            _timeUI.Update(delta);
            _moneyUI.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            var transform = parentTransform + new Transform2(_location);
            _timeUI.Draw(transform);
            _togglePadUI.Draw(transform);
            _moneyUI.Draw(transform);
        }
    }
}
