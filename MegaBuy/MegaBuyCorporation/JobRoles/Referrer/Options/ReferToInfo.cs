using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.JobRoles.Referrer.Options
{
    public sealed class ReferToInfo : ICallOption
    {
        public string Description => "Info";

        public void Go()
        {
            World.Publish(new CallResolved(CallResolution.ReferToInfo));
        }
    }
}
