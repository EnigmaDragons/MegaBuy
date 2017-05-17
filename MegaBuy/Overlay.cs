using System;
using MegaBuy.Money;
using MegaBuy.Pads;
using MegaBuy.Player.Energy;
using MegaBuy.Player.Hungers;
using MegaBuy.Time;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class Overlay : IVisualAutomaton
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly ClockUI _clockUi;
        private readonly TogglePad _togglePad;
        private readonly MoneyUI _moneyUI;
        private readonly EnergyUI _energyUI;
        private readonly HungerUI _hungerUI;

        public ClickUIBranch Branch { get; set; }

        public Overlay()
        {
            // @todo #1 update toggle button colors
            Branch = new ClickUIBranch("Overlay", (int)ClickUIPriorities.Overlay);
            _clockUi = new ClockUI();
            _togglePad = new TogglePad();
            _moneyUI = new MoneyUI();
            _energyUI = new EnergyUI();
            _hungerUI = new HungerUI();
            Branch.Add(_togglePad.Branch);
        }

        public void Update(TimeSpan delta)
        {
            _clockUi.Update(delta);
            _moneyUI.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _clockUi.Draw(absoluteTransform);
            _togglePad.Draw(absoluteTransform);
            _moneyUI.Draw(absoluteTransform);
            _energyUI.Draw(absoluteTransform);
            _hungerUI.Draw(absoluteTransform);
        }
    }
}
