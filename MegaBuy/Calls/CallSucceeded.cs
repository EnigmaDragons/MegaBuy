
namespace MegaBuy.Calls
{
    public sealed class CallSucceeded
    {
        public CallRating Rating { get; }

        public CallSucceeded(CallRating rating)
        {
            Rating = rating;
        }
    }
}
