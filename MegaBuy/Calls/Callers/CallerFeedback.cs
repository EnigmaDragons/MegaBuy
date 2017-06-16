using MegaBuy.Calls.Ratings;

namespace MegaBuy.Calls.Callers
{
    public sealed class CallerFeedback
    {
        private CallRating _rating;

        public int RatingScore => _rating.AsInt();
        public string Review { get; private set; }

        public CallerFeedback(CallRating rating, string review)
        {
            _rating = rating;
            Review = review;
        }
    }
}
