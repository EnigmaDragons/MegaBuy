
namespace MegaBuy.Calls
{
    public struct CallRated
    {
        public int CallId { get; }
        public CallRating Rating { get; }

        public CallRated(int callId, CallRating callRating)
        {
            CallId = callId;
            Rating = callRating;
        }
    }
}
