using System.Collections.Generic;
using MonoDragons.Core.Engine;

namespace MegaBuy.Shopping.Foods
{
    public class McKingJrs : IShoppingCompany
    {
        public string Name => "McKing Jr's";
        public string Description => "We make the foods you crave!";
        public List<IItem> Items { get; } = new List<IItem>
        {
            new Food("Value Burger", "Do you want a burger but can't afford a real one? Well we can assure you this Value \"Burger\" tastes just like the real thing.", new ItemCost(5), 25),
        };

        public void Buy(IItem item)
        {
            var food = (Food) item;
            World.Publish(new FoodEaten(food));
            GameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
