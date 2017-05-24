using System.Collections.Generic;
using MonoDragons.Core.Engine;

namespace MegaBuy.Shopping.Foods
{
    public class McKingJrs : IShoppingCompany
    {
        private readonly GameState _gameState = CurrentGameState.State;
        public string Name => "McKing Jr's";
        public string Description => "We make the foods you crave!";
        public List<IItem> Items { get; } = new List<IItem>
        {
            new Food("Value Dog", new ItemCost(10), 5, -2, "The only way it would taste better, is if you stole it."),
            new Food("Jr. Burrito", new ItemCost(15), 8, -1, "It's like a burrito, but smaller."),
            new Food("Prince Burger", new ItemCost(40), 22, 2, "One burger to rule them all!"),
            new Food("Deli KingWich", new ItemCost(32), 18, 0, "A single bite and you'll feel like royalty."),
            new Food("McTaco", new ItemCost(24), 13, 1, "We tried really hard to think outside the bun."),
            new Food("Ham Sub Royale", new ItemCost(50), 30, 2, "It's like an ordinary Ham Sub, taken to the next level!"),
        };

        public void Buy(IItem item)
        {
            var food = (Food) item;
            World.Publish(new FoodEaten(food));
            _gameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
