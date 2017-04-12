using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Food
{
    public class FoodOptionUI : IVisual
    {
        private readonly List<IVisual> _visuals;

        public FoodOptionUI(Food food, ClickUIBranch layer, int i)
        {
            var x = (int) (i%4)*300;
            var y = (i/4)*300;
            var button = new SingleImageButton(
                "Images/Food/" + food.Image,
                Color.FromNonPremultiplied(0, 0, 0, 50),
                Color.FromNonPremultiplied(0, 0, 0, 100),
                new Transform2(new Vector2(x, y), new Size2(200, 200)),
                () => World.Publish(new FoodOrdered(food)));
            layer.Add(button);
            _visuals = new List<IVisual>();
            _visuals.Add(button);
            _visuals.Add(new Label
            {
                BackgroundColor = Color.FromNonPremultiplied(0, 0, 0, 0),
                Text = food.Name,
                Transform = new Transform2(new Vector2(x, y), 
                new Size2(200, 30))
            });
            _visuals.Add(new Label
            {
                BackgroundColor = Color.FromNonPremultiplied(0, 0, 0, 0),
                Text = $"MBit - {food.Cost.Amount()}",
                Transform = new Transform2(new Vector2(x, y + 170), 
                new Size2(200, 30))
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
