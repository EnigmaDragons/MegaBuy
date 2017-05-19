using System.Collections.Generic;
using MonoDragons.Core.Engine;

namespace MegaBuy.Shopping.Foods
{
    public sealed class SweetBakery : IShoppingCompany
    {
        private readonly GameState _gameState = CurrentGameState.State;
        public string Name => "Sweet Bakery";
        public string Description => "The newest old-fashioned sugary delights that you need to feel like your normal self.";

        public List<IItem> Items => new List<IItem>
        {
            new Food("Blueberry Cheesecake", new ItemCost(58), 5, 11, 
                "Rich, delectable cheescake, topped with blueberries, and a hint of lemon zest and pretensiousness"),
            new Food("Chocolate Cookie", new ItemCost(18), 2, 4, 
                "Just like your grandmother would have made, if she were 20 years younger and not allergic to chocolate"),
            new Food("Giant Ice Cream Sandwich", new ItemCost(64), 3, 13, 
                "It's the coolest, sweetest thing you'll find anywhere in bitspace"),
            new Food("Strawberry Roll", new ItemCost(29), 2, 6,
                "The best treat you can bring to your role-playing group."),
            new Food("Vanilla Cupcake", new ItemCost(44), 3, 9,
                "Our vanilla cupcakes provide so much more than a vanilla experience!"),
            new Food("Strawberry Mini Cake", new ItemCost(80), 5, 16,
                "This cake is small enough that you ought to buy at least 2. Or more."),
        };

        public void Buy(IItem item)
        {
            var food = (Food)item;
            World.Publish(new FoodEaten(food));
            _gameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
