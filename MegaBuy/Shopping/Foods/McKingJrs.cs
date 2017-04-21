using System.Collections.Generic;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Shopping.Foods
{
    public class McKingJrs : IShoppingCompany
    {
        public string Name => "McKing Jr's";
        public string Description => "We make the foods you crave!";
        public ClickUIBranch Branch { get; }

        private readonly List<Food> _items = new List<Food>
        {
            new Food("Value Burger", "Do you want a burger but can't afford a real one? Well we can assure you this Value \"Burger\" tastes just like the real thing.", new ItemCost(5), 25),
        };

        private readonly Transform2 _transform = Transform2.Zero;
        private readonly List<ShoppingItemUI> _itemsUI = new List<ShoppingItemUI>();

        public McKingJrs()
        {
            Branch = new ClickUIBranch("McKing Jr's", (int)ClickUIPriorities.Pad);
            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                var option = new ShoppingItemUI(item, i, () => Buy(item));
                _itemsUI.Add(option);
                Branch.Add(option.Branch);
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _itemsUI.ForEach(x => x.Draw(absoluteTransform));
        }

        private void Buy(Food food)
        {
            World.Publish(new FoodEaten(food));
            GameState.PlayerAccount.Remove(food.Cost);
        }
    }
}
