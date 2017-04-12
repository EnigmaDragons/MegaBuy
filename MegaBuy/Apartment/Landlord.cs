using System;
using MegaBuy.Money;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Apartment
{
    public sealed class Landlord
    {
        private readonly Rent _currentRent;
        private PlayerAccount _rentersAccount;

        private bool _rentPaidToday;

        public Landlord(Rent rent, PlayerAccount acct)
        {
            _currentRent = rent;
            _rentersAccount = acct;
            World.Subscribe(EventSubscription.Create<DayEnded>(IncreaseRent, this));
            World.Subscribe(EventSubscription.Create<RentPaid>(RentPaid, this));
        }

        private void IncreaseRent(DayEnded dayended)
        {
            if (!_rentPaidToday)
                World.NavigateToScene("Evicted");
            _currentRent.IncreaseByPercent(Convert.ToDecimal(0.15));
            _rentPaidToday = false;
        }

        private void RentPaid(RentPaid rentPaid)
        {
            if(_rentersAccount.Amount() > _currentRent.Amount())
            {
                _rentersAccount.Remove(new Rent(_currentRent.Amount()));
                _rentPaidToday = true;
            }
            else
                World.NavigateToScene("Evicted");
            
        }
    }
}
