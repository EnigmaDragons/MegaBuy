using System;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls
{
    public sealed class Caller : IAutomaton
    {
        public CallerPatience Patience = CallerStartingPatience.New;

        private readonly int _patienceLossRateMs;
        private int _gracePeriods;

        private double _elapsedMs;

        public Caller(int patienceLossRateMs)
        {
            _patienceLossRateMs = patienceLossRateMs;
            _gracePeriods = 3;
        }

        public void Update(TimeSpan delta)
        {
            _elapsedMs += delta.TotalMilliseconds;
            UpdatePatience();
            if (Patience.Value == 0)
                World.Publish(new CallFailed());
        }

        private void UpdatePatience()
        {
            if (_elapsedMs < _patienceLossRateMs) return;

            _elapsedMs -= _patienceLossRateMs;
            if (_gracePeriods > 0)
            {
                _gracePeriods--;
                return;
            }

            Patience.Reduce();
        }
    }
}
