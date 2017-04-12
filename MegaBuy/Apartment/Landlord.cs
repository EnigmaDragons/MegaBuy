using System;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Apartment
{
    public sealed class Landlord
    {
        private readonly Rent _currenRent;

        public bool RentPaidToday { get; private set; } = false;

        public string RentDue => "24:00";
        public string RentAmount => _currenRent.Amount().ToString("0.##");

        public Landlord(Rent rent)
        {
            _currenRent = rent;
            World.Subscribe(EventSubscription.Create<DayEnded>(IncreaseRent, this));
            World.Subscribe(EventSubscription.Create<RentPaid>(RentPaid, this));
        }

        private void IncreaseRent(DayEnded dayended)
        {
            if (!RentPaidToday)
                World.NavigateToScene("Evicted");
            _currenRent.IncreaseByPercent(Convert.ToDecimal(0.15));
            RentPaidToday = false;
        }

        private void RentPaid(RentPaid rentPaid)
        {
            // @todo #1 Deduct rent amount from player's account
            RentPaidToday = true;
        }
    }
}
