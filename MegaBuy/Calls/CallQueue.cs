using System.Threading.Tasks;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls
{
    public sealed class CallQueue
    {
        public CallQueue()
        {
            World.Subscribe(EventSubscription.Create<AgentCallStatusChanged>(PlayerAvailable, this));
        }

        private async void PlayerAvailable(AgentCallStatusChanged statusChanged)
        {
            if (!statusChanged.Status.Equals(AgentCallStatus.Available)) return;

            await Task.Delay(Rng.Int(0, 5) * 1000);
            World.Publish(new CallStarted(new CallGenerater(Rules.CallCenterPosition.Referrer).GenerateCall()));
        }
    }
}
