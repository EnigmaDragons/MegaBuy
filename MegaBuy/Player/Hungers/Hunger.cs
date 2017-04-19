using System;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Foods;
using MegaBuy.Shopping;

namespace MegaBuy.Player.Hungers
{
    public sealed class Hunger : IAutomaton
    {
        private readonly int _hungerPerHour;

        private bool _hungerChanged;
        private decimal _hunger;

        public Hunger()
            : this (6) { }

        public Hunger(int hungerPerHour)
        {
            _hungerPerHour = hungerPerHour;
            World.Subscribe(EventSubscription.Create<MinuteChanged>(IncreaseHunger, this));
            World.Subscribe(EventSubscription.Create<FoodEaten>(EatFood, this));
        }

        private void EatFood(FoodEaten foodEaten)
        {
            _hunger -= foodEaten.Food.HungerRecovery.Amount;
        }

        private void IncreaseHunger(MinuteChanged hourChanged)
        {
            if(_hunger + _hungerPerHour / 60 >= Math.Ceiling(_hunger))
                _hungerChanged = true;
            _hunger += _hungerPerHour / 60;
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
