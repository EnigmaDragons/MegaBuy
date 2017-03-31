using MonoDragons.Core.Engine;

namespace MegaBuy.Money
{
    public class Rent: IRent, IAmount
    {
        private decimal _amount;

        protected Rent(decimal initial)
        {
            _amount = initial;
        }

        public void Pay(IAmount amount)
        {
            World.Publish(new RentPaid());
        }

        public void Increase(IAmount amount)
        {
            _amount += amount.Amount();
            World.Publish(new RentIncreased());
        }

        public decimal Amount()
        {
            return _amount;
        }
    }
}