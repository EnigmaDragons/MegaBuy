using System;
using MegaBuy.Calls.Rules;

namespace MegaBuy.Calls.Ratings
{
    public struct CallRating
    {
        private readonly int _rating;

        public CallRating(int value)
        {
            if (value > 5 || value < 1)
                throw new ArgumentException("Invalid Call Rating");
            _rating = value;
        }

        public int AsInt()
        {
            return _rating;
        }

        public decimal PaymentMultiplier()
        {
            return CallRatingPaymentMultipliers.Get(this);
        }
    }
}
