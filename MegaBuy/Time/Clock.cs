using System;
using MonoDragons.Core.Engine;

namespace MegaBuy.Time
{
    public sealed class Clock : IAutomaton
    {
        private readonly Timer _timer;

        private int _day;
        private int _hour;
        private int _minute;

        public string Time => $"{_hour:D2}:{_minute:D2}";
        
        public Clock()
            : this(80, 8, 0) { }

        public Clock(int msPerMinute, int hour, int minute)
        {
            _timer = new Timer(IncrementMinute, msPerMinute);
            _hour = hour;
            _minute = minute;
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
        }

        private void IncrementMinute()
        {
            if (_minute == 59)
                IncrementHour();
            _minute = (_minute + 1) % 60;
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
