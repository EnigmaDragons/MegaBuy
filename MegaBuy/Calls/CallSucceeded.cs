
namespace MegaBuy.Calls
{
    public struct CallSucceeded
    {
        public int CallId { get; }

        public CallSucceeded(int callId)
        {
            CallId = callId;
        }
    }
}
