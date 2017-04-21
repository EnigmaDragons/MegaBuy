using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.JobRoles.Referrer.Options
{
    public class ReferToReturns : ICallOption
    {
        public string Description => "Returns";

        public void Go()
        {
            World.Publish(new CallResolved(CallResolution.ReferToReturns));
        }
    }
}
