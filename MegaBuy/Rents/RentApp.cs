using System;
using MegaBuy.Apartment;
using MegaBuy.Money.Accounts;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Rents
{
    public class RentApp : IApp
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(800, Sizes.Margin));
        private readonly ImageLabel _amountDue;
        private readonly ImageLabel _paymentDueBy;
        private readonly ImageTextButton _payButton;
        private readonly ImageLabel _disabledPayButton;
        private readonly PlayerAccount _playerAccount;
        private readonly Landlord _landlord;

        private bool _canPayRent;

        public App Type => App.Rent;
        public ClickUIBranch Branch { get; private set; }
        
        public RentApp()
        {
            _playerAccount = GameState.PlayerAccount;
            _landlord = GameState.Landlord;
            Branch = new ClickUIBranch("Rent App", (int)ClickUIPriorities.Pad);
            _amountDue = new ImageLabel("", "Images/UI/label", new Transform2(new Vector2(-(Sizes.Label.Width / 2), 0), Sizes.Label));
            _paymentDueBy = new ImageLabel("", "Images/UI/label", new Transform2(new Vector2(-(Sizes.Label.Width / 2), Sizes.Label.Height + Sizes.Margin), Sizes.Label));
            _payButton = ImageTextButtonFactory.Create("PAY", new Vector2(-(Sizes.Button.Width / 2), (Sizes.Label.Height + Sizes.Margin) * 2), () => World.Publish(new RentPaid()));
            _disabledPayButton = new ImageLabel("PAY", "Images/UI/button-disable", new Transform2(new Vector2(-(Sizes.Button.Width / 2), (Sizes.Label.Height + Sizes.Margin) * 2), Sizes.Button));
        }

        public void Update(TimeSpan delta)
        {
            _amountDue.Text = $"Amount Due: {_landlord.RentAmountStr}";
            _paymentDueBy.Text = $"Payment Due By: {_landlord.RentDue}";
            if (!_canPayRent && _playerAccount.Amount() >= _landlord.RentAmount && !_landlord.RentPaidToday)
            {
                Branch.Add(_payButton);
                _canPayRent = true;
            }
            if (_canPayRent && _playerAccount.Amount() < _landlord.RentAmount && _landlord.RentPaidToday)
            {
                Branch.Remove(_payButton);
                _canPayRent = false;
            } 
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_landlord.RentPaidToday)
                return;
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _amountDue.Draw(absoluteTransform);
            _paymentDueBy.Draw(absoluteTransform);
            if (_playerAccount.Amount() >= _landlord.RentAmount)
                _payButton.Draw(absoluteTransform);
            else
                _disabledPayButton.Draw(absoluteTransform);
        }
    }
}
