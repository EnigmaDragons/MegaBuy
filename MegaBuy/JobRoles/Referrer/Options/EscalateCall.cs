using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.JobRoles.Referrer.Options
{
    public class EscalateCall : ICallOption
    {
        public string Description => "Escalate";

        public void Go()
        {
            World.Publish(new CallResolved(CallResolution.EscalateCall));
        }
    }
}
