using System.Collections.Generic;

namespace MegaBuy.Calls.Rules
{
    public class CallerPatienceCallRatings
    {
        private static readonly Dictionary<int, int> _ratings = new Dictionary<int, int>
        {
            {15, 5},
            {14, 4},
            {13, 4},
            {12, 3},
            {11, 3},
            {10, 3},
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
