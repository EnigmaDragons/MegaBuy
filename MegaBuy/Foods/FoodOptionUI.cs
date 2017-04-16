using System;
using System.Collections.Generic;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Foods
{
    public class FoodOptionUI : IVisual
    {
        private readonly Transform2 _transform;
        private readonly string _food;
        private readonly ImageTextButton _button;

        public ClickUIBranch Branch { get; private set; }

        public FoodOptionUI(Food food, int i)
        {
            Branch = new ClickUIBranch(food.Name, (int)ClickUIPriorities.Pad);
            var x = (i%4) * (Sizes.Food.Width + Sizes.Margin);
            var y = (i/4) * (Sizes.Food.Height + Sizes.Margin * 2 + Sizes.Button.Height);
            _food = food.Name;
            _transform = new Transform2(new Vector2((int)x, (int)y));
            _button = ImageTextButtonFactory.Create("BUY", new Vector2(0, Sizes.Food.Height + Sizes.Margin), () => BuyFood(food));
            Branch.Add(_button);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            World.Draw("Images/Food/" + _food.ToLower().Replace(" ", "-"), absoluteTransform + new Transform2(Sizes.Food));
            _button.Draw(absoluteTransform);
        }

        private void BuyFood(Food food)
        {
            World.Publish(new FoodOrdered(food));
            World.Publish(new FoodEaten(food));
        }
    }
}
