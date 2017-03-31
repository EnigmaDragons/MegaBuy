using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Time
{
    public sealed class Clock : IAutomaton
    {
        private readonly Timer _timer;
        private readonly Label _label;

        private int _day;
        private int _hour;
        private int _minute;

        public Clock(Label label)
            : this(1200, label) { }

        public Clock(int msPerMinute, Label label)
        {
            _label = label;
            _timer = new Timer(IncrementMinute, msPerMinute);
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
            _label.Text = $"{_hour:D2}:{_minute:D2}";
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
        }

        private void IncrementDay()
        {
            World.Publish(new DayEnded(_day));
            _day++;
            World.Publish(new DayStarted(_day));
        }
    }
}
