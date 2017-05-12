using System.Threading.Tasks;
using MegaBuy.Calls.Events;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Calls.Rules;

namespace MegaBuy.Calls
{
    public sealed class CallQueue
    {
        private CallGenerator generator;

        public CallQueue()
        {
            World.Subscribe(EventSubscription.Create<AgentCallStatusChanged>(PlayerAvailable, this));
            generator = new CallGenerator(Rules.JobRole.ReferrerLevel1);
        }

        private async void PlayerAvailable(AgentCallStatusChanged statusChanged)
        {
            if (!statusChanged.Status.Equals(AgentCallStatus.Available)) return;
            await Task.Delay(Rng.Int(0, 5) * 1000);
            World.Publish(new CallStarted(generator.GenerateCall()));
        }

        public void ChangePlayerRole(JobRole role)
        {
            generator.PositionChanged(role);
        }
    }
}
