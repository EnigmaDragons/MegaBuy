
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Ratings;

namespace MegaBuy.Calls.Events
{
    public sealed class CallRated
    {
        public int CallId { get; }
        public int RatingScore => Feedback.RatingScore;
        public CallerFeedback Feedback { get; }
        public CallRating Rating => Feedback.Rating;

        public CallRated(int callId, CallerFeedback feedback)
        {
            CallId = callId;
            Feedback = feedback;
        }
    }
}
