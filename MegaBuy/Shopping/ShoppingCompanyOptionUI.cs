﻿using System;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Shopping
{
    public class ShoppingCompanyOptionUI : IVisual
    {
        private readonly Transform2 _transform;
        private readonly ImageWithDescription _productDetails;
        private readonly Label _label;
        private readonly ImageTextButton _button;

        public ClickUIBranch Branch { get; private set; }

        public ShoppingCompanyOptionUI(IShoppingCompany company, int i, Action whenBought)
        {
            Branch = new ClickUIBranch(company.Name, (int)ClickUIPriorities.Pad);
            var x = (i % 4) * (Sizes.Item.Width + Sizes.Margin);
            var y = (i / 4) * (Sizes.Item.Height + Sizes.Margin * 2 + Sizes.Button.Height);
            _transform = new Transform2(new Vector2((int)x, (int)y));
            _productDetails = new ImageWithDescription("Images/Companies/" + company.Name.ToLower().Replace(" ", "-").Replace(".", "").Replace("'", ""), company.Description, new Transform2(Sizes.Item));
            _label = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/14",
                Transform = new Transform2(new Vector2(0, Sizes.Item.Height + 5), new Size2(Sizes.Item.Width, 30)),
                RawText = company.Name
            };
            _button = ImageTextButtonFactory.Create("Shop", new Vector2(0, Sizes.Item.Height + Sizes.SmallMargin + 30), whenBought);
            Branch.Add(_button);
            Branch.Add(_productDetails);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _productDetails.Draw(absoluteTransform);
            _label.Draw(absoluteTransform);
            _button.Draw(absoluteTransform);
        }
    }
}
