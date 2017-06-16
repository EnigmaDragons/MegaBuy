using System.Collections.Generic;
using MegaBuy.Calls.Ratings;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Callers
{
    public static class CallerReviews
    {
        private static List<string> OneStarReviews { get; } = new List<string>
        {
            "MegaBuy's customer support is the worst!",
            "I will never be purchasing from them again. Garbage customer service.",
            "Total rip off! They don't even honor their own return policy.",
        };

        private static List<string> TwoStarReviews { get; } = new List<string>
        {
            "The customer rep was slow and unresponsive.",
            "I was helped, but MegaBuy doesn't seem to hire the best people.",
            "Could be better. could be worse.",
        };

        private static List<string> ThreeStarReviews { get; } = new List<string>
        {
            "Best online purchase of my life! I had a problem with my order and they helped me instantly.",
            "I was very frustrated with a defective product. The support rep comforted me and solved my problem.",
            "Great experience!",
        };

        private static readonly Map<int, List<string>> ReviewsByScore = new Map<int, List<string>>
        {
            { 1, OneStarReviews },
            { 2, TwoStarReviews },
            { 3, ThreeStarReviews }
        };

        public static string Get(CallRating rating)
        {
            return Get(rating.AsInt());
        }

        public static string Get(int ratingScore)
        {
            return ReviewsByScore[ratingScore].Random();
        }
    }
}
