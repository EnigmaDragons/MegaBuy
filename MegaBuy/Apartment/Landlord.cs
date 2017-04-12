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

        public Landlord(Rent rent)
        {
            _currenRent = rent;
            World.Subscribe(EventSubscription.Create<DayEnded>(IncreaseRent, this));
        }

        private void IncreaseRent(DayEnded dayended)
        {
            _currenRent.IncreaseByPercent(Convert.ToDecimal(0.15));
        }
    }
}
