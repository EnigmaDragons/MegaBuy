using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Options
{
    public sealed class ReferToInfo : ICallOption
    {
        public string Description => "Refer the caller to information.";

        public void Go()
        {
            World.Publish(new CallResolved(CallResolution.ReferToInfo));
        }
    }
}
