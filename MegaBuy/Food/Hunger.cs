using System;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Food
{
    public sealed class Hunger : IAutomaton
    {
        private readonly int _hungerPerHour;

        private bool _hungerChanged;
        private int _hunger;

        public Hunger()
            : this (6) { }

        public Hunger(int hungerPerHour)
        {
            _hungerPerHour = hungerPerHour;
            World.Subscribe(EventSubscription.Create<HourChanged>(IncreaseHunger, this));
        }

        private void IncreaseHunger(HourChanged hourChanged)
        {
            _hunger += _hungerPerHour;
            _hungerChanged = true;
        }

        public void Update(TimeSpan delta)
        {
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
            _hungerChanged = false;
        }
    }
}
