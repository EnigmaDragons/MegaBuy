using System;
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
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly TimeUI _timeUI;
        private readonly TogglePadUI _togglePadUI;
        private readonly MoneyUI _moneyUI;
        private readonly ClickUIBranch _branch;

        public OverlayUI(ClickUIBranch parentBranch)
        {
            //@todo #1 update toggle button colors and label colors
            _branch = new ClickUIBranch("Overlay", (int)ClickUIPriorities.Overlay);
            parentBranch.Add(_branch);
            _timeUI = new TimeUI();
            _togglePadUI = new TogglePadUI(_branch);
            _moneyUI = new MoneyUI();
        }

        public void Update(TimeSpan delta)
        {
            _timeUI.Update(delta);
            _moneyUI.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _branch.ParentLocation = absoluteTransform.Location;
            _timeUI.Draw(absoluteTransform);
            _togglePadUI.Draw(absoluteTransform);
            _moneyUI.Draw(absoluteTransform);
        }
    }
}
