using System;
using MegaBuy.Money;
using MegaBuy.Pads;
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
        private readonly ClockUI _clockUi;
        private readonly TogglePad _togglePad;
        private readonly MoneyUI _moneyUI;
        private readonly ClickUIBranch _branch;

        public OverlayUI(ClickUIBranch parentBranch)
        {
            // @todo #1 update toggle button colors and label colors
            _branch = new ClickUIBranch("Overlay", (int)ClickUIPriorities.Overlay);
            parentBranch.Add(_branch);
            _clockUi = new ClockUI();
            _togglePad = new TogglePad(_branch);
            _moneyUI = new MoneyUI();
        }

        public void Update(TimeSpan delta)
        {
            _clockUi.Update(delta);
            _moneyUI.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _branch.ParentLocation = absoluteTransform.Location;
            _clockUi.Draw(absoluteTransform);
            _togglePad.Draw(absoluteTransform);
            _moneyUI.Draw(absoluteTransform);
        }
    }
}
