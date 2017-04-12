using System;
using MegaBuy.Money;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Apartment
{
    public sealed class Landlord
    {
        private readonly Rent _currenRent;

        private bool _rentPaidToday;

        public Landlord(Rent rent)
        {
            _currenRent = rent;
            World.Subscribe(EventSubscription.Create<DayEnded>(IncreaseRent, this));
            World.Subscribe(EventSubscription.Create<RentPaid>(RentPaid, this));
        }

        private void IncreaseRent(DayEnded dayended)
        {
            if (!_rentPaidToday)
                World.NavigateToScene("Evicted");
            _currenRent.IncreaseByPercent(Convert.ToDecimal(0.15));
            _rentPaidToday = false;
        }

        private void RentPaid(RentPaid rentPaid)
        {
            // @todo #1 Deduct rent amount from player's account
            _rentPaidToday = true;
        }
    }
}
