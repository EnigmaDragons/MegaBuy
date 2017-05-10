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
            // @todo #1 Add clever item descriptions
            new Food("Value Dog", new ItemCost(10), 5, ""),
            new Food("Jr. Burrito", new ItemCost(15), 8, ""),
            new Food("Prince Burger", new ItemCost(40), 22, ""),
            new Food("Deli KingWich", new ItemCost(32), 18, ""),
            new Food("McTaco", new ItemCost(24), 13, ""),
            new Food("Ham Sub Royale", new ItemCost(50), 30, ""),
        };

        public void Buy(IItem item)
        {
            var food = (Food) item;
            World.Publish(new FoodEaten(food));
            GameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
