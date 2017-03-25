using System;
using MonoDragons.Core.Engine;

namespace MegaBuy.Player
{
    public sealed class Hunger : IAutomaton
    {
        private readonly Timer _timer;
        private int _hunger;

        public Hunger()
            : this(6000) { }

        public Hunger(int msPerHungerGain)
        {
            _timer = new Timer(() => _hunger++, msPerHungerGain);
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
            if (_hunger >= 100)
                World.NavigateToScene("Starved");
        }
    }
}
