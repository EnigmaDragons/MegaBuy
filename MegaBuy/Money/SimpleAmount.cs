
namespace MegaBuy.Money
{
    public abstract class SimpleAmount : IAmount
    {
        private readonly decimal _amount;

        protected SimpleAmount(decimal amount)
        {
            _amount = amount;
        }

        public decimal Amount()
        {
            return _amount;
        }
    }
}
