using System.Collections.Generic;
using System.Linq;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Foods
{
    public class FoodPage : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(350, Sizes.Margin));
        private readonly List<FoodOptionUI> _foods = new List<FoodOptionUI>();

        public ClickUIBranch Branch { get; private set; }

        public FoodPage(params Food[] foods)
        {
            Branch = new ClickUIBranch("Food Page", (int)ClickUIPriorities.Pad);
            for (var i = 0; i < foods.Length; i++)
            {
                var option = new FoodOptionUI(foods[i], i);
                _foods.Add(option);
                Branch.Add(option.Branch);
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _foods.ForEach(x => x.Draw(absoluteTransform));
        }
    }
}
