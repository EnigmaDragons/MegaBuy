using System;
using MegaBuy.Money;
using MegaBuy.Money.Accounts;
using MegaBuy.Rents;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Apartment
{
    public sealed class Landlord
    {
        private readonly Rent _currentRent;
        private PlayerAccount _rentersAccount;

        public bool RentPaidToday { get; private set; } = false;

        public decimal RentAmount => _currentRent.Amount();
        public string RentDue => "24:00";
        public string RentAmountStr => _currentRent.Amount().ToString("0.##");

        public Landlord(Rent rent, PlayerAccount acct)
        {
            _currentRent = rent;
            _rentersAccount = acct;
            World.Subscribe(EventSubscription.Create<DayEnded>(IncreaseRent, this));
            World.Subscribe(EventSubscription.Create<RentPaid>(RentPaid, this));
        }

        private void IncreaseRent(DayEnded dayended)
        {
            if (!RentPaidToday)
                World.NavigateToScene("Evicted");
            _currentRent.IncreaseByPercent(Convert.ToDecimal(0.15));
            RentPaidToday = false;
        }

        private void RentPaid(RentPaid rentPaid)
        {
            if(_rentersAccount.Amount() > _currentRent.Amount())
            {
                _rentersAccount.Remove(new Rent(_currentRent.Amount()));
                RentPaidToday = true;
            }
            else
                World.NavigateToScene("Evicted");
        }
    }
}
