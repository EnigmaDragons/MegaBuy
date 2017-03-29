using MegaBuy.Calls.Rules;

namespace MegaBuy.Calls
{
    public sealed class CallResolved
    {
        public CallResolution Resolution { get; }

        public CallResolved(CallResolution resolution)
        {
            Resolution = resolution;
        }
    }
}
