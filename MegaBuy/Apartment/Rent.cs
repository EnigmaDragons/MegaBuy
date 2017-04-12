using MegaBuy.Money;
using MonoDragons.Core.Engine;

namespace MegaBuy.Apartment
{
    public class Rent : IRent, IAmount
    {
        private decimal _amount;

        public Rent(decimal initial)
        {
            _amount = initial;
        }

        public void Increase(IAmount amount)
        {
            _amount += amount.Amount();
            World.Publish(new RentIncreased());
        }

        public void IncreaseByPercent(decimal percent)
        {
            _amount += _amount * percent;
            World.Publish(new RentIncreased());
        }

        public decimal Amount()
        {
            return _amount;
        }
    }
}