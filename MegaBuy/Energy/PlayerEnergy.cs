using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System;

namespace MegaBuy.Energy
{
    public class PlayerEnergy
    {
        private readonly int _energyUsedPerHour;
        private readonly int _energyPerHourSlept;
        private decimal _energy = 80;
        private bool _energyChanged;
        private bool _isExhausted;
        public PlayerEnergy()
            : this (4, 6) { }
        
        public PlayerEnergy(int energyUsedPerHour, int energyPerHourSlept)
        {
            _energyUsedPerHour = energyUsedPerHour;
            _energyPerHourSlept = energyPerHourSlept;
            World.Subscribe(EventSubscription.Create<MinuteChanged>(DecreaseEnergy, this));
        }

        public void FellASleep()
        {
            _isExhausted = true;
            Sleep();
        }

        public void Sleep()
        {
            World.Unsubscribe(this);
            World.Subscribe(EventSubscription.Create<HourChanged>(IncreaseEnergy, this));
        }

        private void DecreaseEnergy(MinuteChanged hourChanged)
        {
            if (_energy - _energyUsedPerHour / 60 <= Math.Floor(_energy))
                _energyChanged = true;
            _energy -= _energyUsedPerHour / 60;
        }

        private void IncreaseEnergy(HourChanged hourChanged)
        {
            _energy += _isExhausted ? _energyPerHourSlept : _energyPerHourSlept * new decimal(0.55);
            _energyChanged = true;
            if (_energy >= 70)
            {
                _isExhausted = false;
                World.Publish(new Awaken());
                World.Unsubscribe(this);
                World.Subscribe(EventSubscription.Create<MinuteChanged>(DecreaseEnergy, this));
            }

        }

        public void Update(TimeSpan delta)
        {
            if (!_energyChanged)
                return;
            else if (_energy <= 25)
            {
                World.Publish(new FellASleep());
                FellASleep();
            }
            else
                World.Publish(new NotTired());
            _energyChanged = false;
        }
    }
}
