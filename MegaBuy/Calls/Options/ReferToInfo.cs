using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Options
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
