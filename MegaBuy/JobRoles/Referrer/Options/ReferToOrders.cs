using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MonoDragons.Core.Engine;

namespace MegaBuy.JobRoles.Referrer.Options
{
    public sealed class ReferToOrders : ICallOption
    {
        public string Description => "Orders";

        public void Go()
        {
            World.Publish(new CallResolved(Calls.Rules.CallResolution.ReferToOrders));
        }
    }
}
