namespace MegaBuy.Money.Amounts
{
    public class PerCallRate : IAmount
    {
        private readonly decimal _rate;

        public PerCallRate(decimal rate)
        {
            _rate = rate;
        }

        public decimal Amount()
        {
            return _rate;
        }
    }
}