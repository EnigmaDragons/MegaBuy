using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Time
{
    public sealed class Clock : IAutomaton
    {
        private readonly Timer _timer;

        private float _currentRate = 1.0f;

        private int _day;
        private int _hour;
        private int _minute;

        public string Time => $"{_hour:D2}:{_minute:D2}";
        
        public Clock()
            : this(400, 8, 0) { }

        public Clock(int msPerMinute, int hour, int minute)
        {
            _timer = new Timer(IncrementMinute, msPerMinute);
            _hour = hour;
            _minute = minute;
            World.Subscribe(EventSubscription.Create<TimeRateChanged>(ChangeRate, this));
        }

        public void Update(TimeSpan delta)
        {
            var relativeTimePassed = TimeSpan.FromMilliseconds(delta.TotalMilliseconds * _currentRate);
            _timer.Update(relativeTimePassed);
        }

        private void ChangeRate(TimeRateChanged rateChanged)
        {
            _currentRate *= rateChanged.Factor;
        }

        private void IncrementMinute()
        {
            if (_minute == 59)
                IncrementHour();
            _minute = (_minute + 1) % 60;
            World.Publish(new MinuteChanged(_hour, _minute));
        }

        private void IncrementHour()
        {
            if (_hour == 23)
                IncrementDay();
            _hour = (_hour + 1) % 24;
            World.Publish(new HourChanged(_hour));
        }

        private void IncrementDay()
        {
            World.Publish(new DayEnded(_day));
            _day++;
            World.Publish(new DayStarted(_day));
        }
    }
}
