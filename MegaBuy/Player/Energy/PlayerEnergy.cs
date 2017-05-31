using System;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Player.Energy
{
    public sealed class PlayerEnergy : IAutomaton
    {
        private const float TimeRateFactorWhileSleeping = 10.0f;

        private readonly int _energyUsedPerHour;
        private readonly int _energyPerHourSlept;

        private decimal _energy = 80;

        private bool _energyChanged;
        private bool _isExhausted;
        private bool _isSleeping;
        private int _currentMinute;
        private int _currentHour;
        private int _hourToAwake;
        private int _minuteToAwake;

        public PlayerEnergy()
            : this (4, 6) { }
        
        public PlayerEnergy(int energyUsedPerHour, int energyPerHourSlept)
        {
            _energyUsedPerHour = energyUsedPerHour;
            _energyPerHourSlept = energyPerHourSlept;
            World.Subscribe(EventSubscription.Create<MinuteChanged>(MinuteChanged, this));
            World.Subscribe(EventSubscription.Create<WentToBed>(WentToBed, this));
            World.Subscribe(EventSubscription.Create<EnergyRecovery>(EnergyRecovered, this));
        }

        private void EnergyRecovered(EnergyRecovery obj)
        {
            _energy = Math.Max(100, Math.Min(0, _energy + obj.Amount)); 
        }

        private void WentToBed(WentToBed wentoToBed)
        {
            _minuteToAwake = _currentMinute;
            _hourToAwake = _currentHour + wentoToBed.HoursToSleep;
            Sleep();
        }

        private void Awaken()
        {
            _isExhausted = false;
            World.Publish(new TimeRateChanged(1f / TimeRateFactorWhileSleeping));
            World.Publish(new Awaken());
        }

        private void CollapseFromExhaustion()
        {
            _isExhausted = true;
            World.Publish(new CollapsedWithExhaustion());
            Sleep();
        }

        private void Sleep()
        {
            _isSleeping = true;
            World.Publish(new TimeRateChanged(TimeRateFactorWhileSleeping));
        }

        private void MinuteChanged(MinuteChanged minuteChanged)
        {
            _currentMinute = minuteChanged.Minute;
            _currentHour = minuteChanged.Hour;
            if (!_isSleeping)
                DecreaseEnergy();
            else if (_isExhausted)
                IncreaseEnergyCollapsedSleep();
            else if (_isSleeping)
                IncreaseEnergyRestfulSleep();
        }

        private void DecreaseEnergy()
        {          
            var energyChange = Convert.ToDecimal(_energyUsedPerHour) / 60;
            if (_energy - energyChange <= Math.Floor(_energy))
                _energyChanged = true;
            _energy -= energyChange;
        }

        private void IncreaseEnergyCollapsedSleep()
        {
            _energy += (_energyPerHourSlept * new decimal(0.55)) / 60;
            _energyChanged = true;
            if (_energy >= 70)
                Awaken();
        }

        private void IncreaseEnergyRestfulSleep()
        {
            _energy = Math.Max(_energy + Convert.ToDecimal(_energyPerHourSlept) / 60, 75);
            _energyChanged = true;
            if (_currentHour.Equals(_hourToAwake) && _currentMinute.Equals(_minuteToAwake))
                Awaken();
        }

        public void Update(TimeSpan delta)
        {
            if (!_energyChanged)
                return;
            if (_energy < 0)
                World.NavigateToScene("HeartAttack");
            else if (_energy <= 25)
                CollapseFromExhaustion();
            else if (_energy <= 35)
                World.Publish(new VeryTired());
            else if (_energy <= 50)
                World.Publish(new Tired());
            else
                World.Publish(new NotTired());
            _energyChanged = false;
        }
    }
}
