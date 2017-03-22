using MegaBuy.Calls;

namespace MegaBuy.Money
{
    public sealed class CallPayment : IAmount
    {
        private readonly IPerCallRate _callRate;
        private readonly CallRating _rating;

        public CallPayment(IPerCallRate callRate, CallRating rating) 
        {
            _callRate = callRate;
            _rating = rating;
        }

        public decimal Amount()
        {
            return _callRate.Amount() * _rating.PaymentMultiplier();
        }
    }
}
