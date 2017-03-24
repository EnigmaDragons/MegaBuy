
namespace MegaBuy.Calls
{
    public struct CallSucceeded
    {
        public CallRating Rating { get; }

        public CallSucceeded(CallRating rating)
        {
            Rating = rating;
        }
    }
}
