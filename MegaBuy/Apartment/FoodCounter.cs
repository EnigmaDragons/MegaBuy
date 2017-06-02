using System;
using MegaBuy.Apartment.Map;
using System.Collections.Generic;
using MegaBuy.Shopping.Foods;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Apartment
{
    public class FoodCounter
    {
        public ClickableTile Tile { get; private set; }
        private List<Food> _foods;

        public FoodCounter(TileLocation location)
        {
            Tile = new ClickableTile(/* @todo: replace with counter texture */ "2/floor", location, true, () => EatFood() ,1);
            _foods = new List<Food>();
            World.Subscribe(EventSubscription.Create<FoodDelivered>((f) => _foods.Add(f.Food), this));
        }

        private void EatFood()
        {
            if (_foods.Count > 0)
            {
                World.Publish(new FoodEaten(_foods[0]));
                _foods.RemoveAt(0);
            }
        }
    }
}
