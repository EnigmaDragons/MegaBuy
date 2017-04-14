using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System;

namespace MegaBuy.Energy
{
    public class PlayerEnergy
    {
        private readonly int _energyUsedPerHour;
        private int _energy = 100;
        private bool _energyChanged;
        public PlayerEnergy()
            : this (8) { }

        public PlayerEnergy(int energyUsedPerHour)
        {
            _energyUsedPerHour = energyUsedPerHour;
            World.Subscribe(EventSubscription.Create<HourChanged>(DecreaseEnergy, this));
        }

        private void DecreaseEnergy(HourChanged hourChanged)
        {
            _energy -= _energyUsedPerHour;
            _energyChanged = true;
        }

        public void Update(TimeSpan delta)
        {
            if (!_energyChanged)
                return;
            else if (_energy <= 20)
                World.Publish(new VeryTired());
            else
                World.Publish(new NotTired());
            _energyChanged = false;
        }
    }
}
