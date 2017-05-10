using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Options
{
    public sealed class CallResolutionOption : ICallOption
    {
        private readonly CallResolution _resolution;

        public string Description { get; }

        public CallResolutionOption(CallResolution resolution, string description)
        {
            _resolution = resolution;
            Description = description;
        }

        public void Go()
        {
            World.Publish(new CallResolved(_resolution));
        }
    }
}
