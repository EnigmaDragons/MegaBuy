using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Options
{
    public class ReferToCareers : ICallOption
    {
        public string Description => "Careers";

        public void Go()
        {
            World.Publish(new CallResolved(CallResolution.ReferToCareers));
        }
    }
}
