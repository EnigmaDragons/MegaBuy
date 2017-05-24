using System;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MegaBuy.Money.Amounts;
using MegaBuy.Money.Accounts;

namespace MegaBuy.Shopping
{
    public class ShoppingItemUI : IVisual
    {
        private readonly Transform2 _transform;
        private readonly ImageWithDescription _productDetails;
        private readonly Label _label;
        private readonly ImageTextButton _button;
        private readonly ImageLabel _disabledButton;
        private readonly PlayerAccount _playerAccount;
        private readonly decimal _amount;

        public ClickUIBranch Branch { get; private set; }

        public ShoppingItemUI(IItem item, int i, Action whenBought)
        {
            _playerAccount = CurrentGameState.State.PlayerAccount;
            Branch = new ClickUIBranch(item.Name, (int)ClickUIPriorities.Pad);
            var x = (i%4) * (Sizes.Item.Width + Sizes.Margin);
            var y = (i/4) * (Sizes.Item.Height + Sizes.Margin * 2 + Sizes.Button.Height);
            _transform = new Transform2(new Vector2((int)x, (int)y));
            _productDetails = new ImageWithDescription("Images/Items/" + item.Name.ToLower().Replace(" ", "-").Replace(".", ""), item.Description, new Transform2(Sizes.Item));
            _label = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/14",
                Transform = new Transform2(new Vector2(0, Sizes.Item.Height + 5), new Size2(Sizes.Item.Width, 30)),
                RawText = item.Name + " - $" + item.Cost.Amount()
            };
            _amount = item.Cost.Amount(); 
            _button = ImageTextButtonFactory.Create("Buy", new Vector2(0, Sizes.Item.Height + Sizes.SmallMargin + 30),
                () => { if (_playerAccount.Amount() >= _amount) whenBought(); });
            _disabledButton = new ImageLabel("Buy", "Images/UI/button-disable", new Transform2(new Vector2(0, Sizes.Item.Height + Sizes.SmallMargin + 30), Sizes.Button));
            Branch.Add(_button);
            Branch.Add(_productDetails);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _productDetails.Draw(absoluteTransform);
            _label.Draw(absoluteTransform);
            if (_playerAccount.Amount() >= _amount)
                _button.Draw(absoluteTransform);
            else
                _disabledButton.Draw(absoluteTransform);
        }
    }
}
