using System;
using MegaBuy.Apartment;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    // @todo #1 Design RentApp appearance 
    public sealed class RentApp : IApp
    {
        private readonly Landlord _landlord;
        private readonly ClickUIBranch _layer;
        private readonly ClickUI _clickUi;

        private readonly ColoredRectangle _rect = new ColoredRectangle { Transform = new Transform2(new Size2(1920, 1080)) };
        private readonly Label _rentDueDate;
        private readonly Label _rentAmount;
        private readonly Label _rentAlreadyPaidLabel;
        private readonly TextButton _payButton;

        private bool _canPay;
        private bool _canPayChanged;

        public App Type => App.Rent;

        public RentApp(ClickUI clickUi)
        {
            _landlord = GameState.Landlord;
            _layer = new ClickUIBranch("RentApp", 1);
            _clickUi = clickUi;
            _rect.Color = Color.FromNonPremultiplied(100, 0, 100, 100);
            _payButton = new TextButton(new Rectangle(100, 400, 400, 100), PayRent, "Pay Rent",
                Color.CadetBlue, Color.Red, Color.Green);
            _rentDueDate = new Label {Transform = new Transform2(new Vector2(100, 100), new Size2(800, 100))};
            _rentAmount = new Label { Transform = new Transform2(new Vector2(100, 200), new Size2(800, 100))};
            _rentAlreadyPaidLabel = new Label { Transform = new Transform2(new Vector2(100, 300), new Size2(800, 100)), Text = "Today's Rent Has Been Paid" };
            clickUi.Add(_layer);
            _layer.Add(_payButton);
            _canPayChanged = true;
        }

        public void Update(TimeSpan delta)
        {
            _canPayChanged = _canPay != !_landlord.RentPaidToday;
            _canPay = !_landlord.RentPaidToday;
            _rentDueDate.Text = $"Payment Due By: {_landlord.RentDue}";
            _rentAmount.Text = $"Amount Due: {_landlord.RentAmount}";
            if (!_canPayChanged)
                return;
            if (_canPay)
                GainedFocus();
            else
                LostFocus();
        }

        public void Draw(Transform2 parentTransform)
        {
            // @todo #1 Fix this terrible evil hack. We should not be changing any values in the Render loop
            _layer.Location = parentTransform.Location;
            _rect.Draw(parentTransform);
            if (!_landlord.RentPaidToday)
            {
                _payButton.Draw(parentTransform);
                _rentDueDate.Draw(parentTransform);
                _rentAmount.Draw(parentTransform);
            }
            else
                _rentAlreadyPaidLabel.Draw(parentTransform);
        }

        private void PayRent()
        {
            World.Publish(new RentPaid());
        }

        public void LostFocus()
        {
            _clickUi.Remove(_layer);
        }

        public void GainedFocus()
        {
            _clickUi.Add(_layer);
        }
    }
}
