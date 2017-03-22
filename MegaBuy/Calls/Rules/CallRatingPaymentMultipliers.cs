using System.Collections.Generic;

namespace MegaBuy.Calls.Rules
{
    public static class CallRatingPaymentMultipliers
    {
        private static readonly Dictionary<int, decimal> _multipliers = new Dictionary<int, decimal>
        {
            {1, 1},
            {2, (decimal) 1.5},
            {3, 2},
            {4, (decimal) 2.5},
            {5, 3}
        };

        public static decimal Get(CallRating rating)
        {
            return _multipliers[rating.AsInt()];
        }
    }
}
