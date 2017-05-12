using System.Collections.Generic;
using MonoDragons.Core.Engine;

namespace MegaBuy.Shopping.Foods
{
    public sealed class SweetBakery : IShoppingCompany
    {
        public string Name => "Sweet Bakery";
        public string Description => "The newest old-fashioned sugary delights that you need to feel like your normal self.";
        public List<IItem> Items => new List<IItem>();

        public void Buy(IItem item)
        {
            var food = (Food)item;
            World.Publish(new FoodEaten(food));
            GameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
