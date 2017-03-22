using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;

namespace MegaBuy.Apartment
{
    public sealed class Clock : IAutomaton
    {
        private readonly int _msPerMinute;
        private readonly MutableDrawnText _mutableDrawnText;

        private int _day;
        private int _hour;
        private int _minute;

        private double _elapsedMs;

        public Clock(MutableDrawnText mutableDrawnText)
            : this(1200, mutableDrawnText) { }

        public Clock(int msPerMinute, MutableDrawnText mutableDrawnText)
        {
            _msPerMinute = msPerMinute;
            _mutableDrawnText = mutableDrawnText;
        }

        public void Update(TimeSpan delta)
        {
            _elapsedMs += delta.TotalMilliseconds;
            if (_elapsedMs > _msPerMinute)
                IncrementMinute();
            _mutableDrawnText.Set($"{_hour:D2}:{_minute:D2}");
        }

        private void IncrementMinute()
        {
            if (_minute == 59)
                IncrementHour();
            _minute = (_minute + 1) % 60;
            _elapsedMs -= _msPerMinute;
        }

        private void IncrementHour()
        {
            if (_hour == 23)
                _day++;
            _hour = (_hour + 1) % 24;
        }
    }
}
