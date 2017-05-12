using System.Collections.Generic;
using MonoDragons.Core.Engine;

namespace MegaBuy.Shopping.Foods
{
    public class McKingJrs : IShoppingCompany
    {
        private readonly GameState _gameState = CurrentGameState.GameState;
        public string Name => "McKing Jr's";
        public string Description => "We make the foods you crave!";
        public List<IItem> Items { get; } = new List<IItem>
        {
            // @todo #1 Add clever item descriptions
            new Food("Value Dog", new ItemCost(10), 5, -2, ""),
            new Food("Jr. Burrito", new ItemCost(15), 8, -1, ""),
            new Food("Prince Burger", new ItemCost(40), 22, 2, ""),
            new Food("Deli KingWich", new ItemCost(32), 18, 0, ""),
            new Food("McTaco", new ItemCost(24), 13, 1, ""),
            new Food("Ham Sub Royale", new ItemCost(50), 30, 2, ""),
        };

        public void Buy(IItem item)
        {
            var food = (Food) item;
            World.Publish(new FoodEaten(food));
            _gameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
