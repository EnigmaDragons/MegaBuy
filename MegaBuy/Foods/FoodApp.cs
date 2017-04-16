using System;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Foods
{
    public class FoodApp : IApp
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly FoodPage _page;

        public App Type => App.Food;
        public ClickUIBranch Branch { get; private set; }

        public FoodApp()
        {
            Branch = new ClickUIBranch("Food App", (int)ClickUIPriorities.Pad);
            _page = new FoodPage(
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25),
                new Food("Value Burger", new FoodCost(5), 25));
            Branch.Add(_page.Branch);
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _page.Draw(absoluteTransform);
        }
    }
}
