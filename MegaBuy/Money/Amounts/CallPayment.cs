using MegaBuy.Calls.Ratings;

namespace MegaBuy.Money.Amounts
{
    public sealed class CallPayment : IAmount
    {
        private readonly IAmount _callRate;
        private readonly CallRating _rating;

        public CallPayment(IAmount callRate, CallRating rating) 
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
