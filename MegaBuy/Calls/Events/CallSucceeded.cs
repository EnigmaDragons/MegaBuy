
namespace MegaBuy.Calls.Events
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
