using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Options
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
