using MegaBuy.Apartment.Map;
using System.Collections.Generic;
using MegaBuy.Shopping.Foods;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Apartment
{
    // @todo #1: Backend: Need to ability to have the counter have food or be empty. 
    public class FoodCounter
    {
        private readonly List<Food> _foods;

        public ClickableTile Tile { get; private set; }

        public FoodCounter(TileLocation location)
        {
            Tile = new ClickableTile("2/table2", location, true, EatFood, 2);
            _foods = new List<Food>();
            World.Subscribe(EventSubscription.Create<FoodDelivered>((f) => _foods.Add(f.Food), this));
        }

        private void EatFood()
        {
            if (_foods.Count > 0)
            {
                // @todo #1: Frontend: Add sound for eating food
                // @todo #1: Frontend: Add animation for eating food
                World.Publish(new FoodEaten(_foods[0]));
                _foods.RemoveAt(0);
            }
        }
    }
}
