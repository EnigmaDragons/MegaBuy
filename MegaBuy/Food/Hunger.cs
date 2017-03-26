using System;
using MonoDragons.Core.Engine;

namespace MegaBuy.Food
{
    public sealed class Hunger : IAutomaton
    {
        private readonly Timer _timer;

        private bool _hungerChanged;
        private int _hunger;

        public Hunger()
            : this(6000) { }

        public Hunger(int msPerHungerGain)
        {
            _timer = new Timer(IncreaseHunger, msPerHungerGain);
        }

        private void IncreaseHunger()
        {
            _hunger++;
            _hungerChanged = true;
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
            if (!_hungerChanged)
                return;
            else if (_hunger >= 100)
                World.NavigateToScene("Starved");
            else if (_hunger >= 80)
                World.Publish(new VeryHungry());
            else if (_hunger >= 30)
                World.Publish(new Hungry());
            else
                World.Publish(new NotHungry());
        }
    }
}
