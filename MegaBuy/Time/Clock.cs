using System;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Time
{
    public sealed class Clock : IAutomaton
    {
        private Timer _timer;
        
        private int Hour => DateTime.Hour;
        private int Minute => DateTime.Minute;

        public string Time => $"{Hour:D2}:{Minute:D2}";
        public string Date => DateTime.ToString(DateFormat.Get);
        public DateTime DateTime { get; private set; }

        public Clock(DateTime start)
            : this(400, start) { }

        public Clock(int msPerMinute, DateTime start)
        {
            DateTime = start;
            _timer = new Timer(IncrementMinute, msPerMinute);
            World.Subscribe(EventSubscription.Create<TimeRateChanged>(ChangeRate, this));
        }

        public void Update(TimeSpan delta)
        {
            var relativeTimePassed = TimeSpan.FromMilliseconds(delta.TotalMilliseconds);
            _timer.Update(relativeTimePassed);
        }

        private void ChangeRate(TimeRateChanged rateChanged)
        {
            _timer = new Timer(IncrementMinute, rateChanged.MsPerMinute);
        }

        private void IncrementMinute()
        {
            var min = Minute;
            var hour = Hour;
            DateTime = DateTime.AddMinutes(1);

            World.Publish(new MinuteChanged(Hour, Minute));
            if (hour == 23 && min == 59)
                IncrementDay();
            if (min == 59)
                World.Publish(new HourChanged(Hour));
        }

        private void IncrementDay()
        {
            World.Publish(new DayEnded());
            World.Publish(new DayStarted(DateTime));
        }
    }
}
