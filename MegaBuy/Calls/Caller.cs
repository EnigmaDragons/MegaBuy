
using System;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls
{
    public class Caller : IAutomaton
    {
        public CallerPatience Patience = CallerStartingPatience.New;
        private int _patienceLossRateMs = 100;

        private double _elapsedMs;

        public void Update(TimeSpan delta)
        {
            _elapsedMs += delta.TotalMilliseconds;
            if (Patience.Value == 0)
                World.Publish(new CallFailed());
        }
    }
}
