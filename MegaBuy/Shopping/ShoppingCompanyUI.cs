using System;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Shopping
{
    public class ShoppingCompanyUI : IVisual
    {
        private readonly Transform2 _transform;
        private readonly string _name;
        private readonly ImageTextButton _button;

        public ClickUIBranch Branch { get; private set; }

        public ShoppingCompanyUI(IShoppingCompany item, int i, Action whenBought)
        {
            Branch = new ClickUIBranch(item.Name, (int)ClickUIPriorities.Pad);
            var x = (i % 4) * (Sizes.Item.Width + Sizes.Margin);
            var y = (i / 4) * (Sizes.Item.Height + Sizes.Margin * 2 + Sizes.Button.Height);
            _name = item.Name;
            _transform = new Transform2(new Vector2((int)x, (int)y));
            _button = ImageTextButtonFactory.Create("SHOP", new Vector2(0, Sizes.Item.Height + Sizes.Margin), whenBought);
            Branch.Add(_button);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            World.Draw("Images/Companies/" + _name.ToLower().Replace(" ", "-"), absoluteTransform + new Transform2(Sizes.Item));
            _button.Draw(absoluteTransform);
        }
    }
}
