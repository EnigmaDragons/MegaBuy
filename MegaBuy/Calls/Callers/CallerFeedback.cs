﻿using MegaBuy.Calls.Ratings;

namespace MegaBuy.Calls.Callers
{
    public sealed class CallerFeedback
    {
        public int RatingScore => Rating.AsInt();
        public string Review { get; }
        public CallRating Rating { get; }

        public CallerFeedback(CallRating rating, string review)
        {
            Rating = rating;
            Review = review;
        }
    }
}
