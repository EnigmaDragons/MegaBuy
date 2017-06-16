using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Ratings;

namespace MegaBuy.Calls.Rules
{
    public class CallerPatienceCallRatings
    {
        private static readonly Dictionary<int, int> _ratings = new Dictionary<int, int>
        {
            {15, 3},
            {14, 3},
            {13, 3},
            {12, 3},
            {11, 3},
            {10, 2},
            {9, 2},
            {8, 2},
            {7, 2},
            {6, 2},
            {5, 1},
            {4, 1},
            {3, 1},
            {2, 1},
            {1, 1}
        };

        public static CallRating Get(CallerPatience patience)
        {
            return new CallRating(_ratings[patience.Value]);
        }
    }
}
