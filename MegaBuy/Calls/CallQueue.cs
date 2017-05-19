using System.Threading.Tasks;
using MegaBuy.Calls.Events;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Jobs;

namespace MegaBuy.Calls
{
    public sealed class CallQueue
    {
        private readonly CallGenerator _generator;

        public CallQueue()
        {
            World.Subscribe(EventSubscription.Create<AgentCallStatusChanged>(PlayerAvailable, this));
            World.Subscribe(EventSubscription.Create<JobChanged>(JobChanged, this));
            _generator = new CallGenerator(Job.ReturnSpecialistLevel1);
        }

        private async void PlayerAvailable(AgentCallStatusChanged statusChanged)
        {
            if (!statusChanged.Status.Equals(AgentCallStatus.Available)) return;
            await Task.Delay(Rng.Int(0, 5) * 1000);
            World.Publish(new CallStarted(_generator.GenerateCall()));
        }

        private void JobChanged(JobChanged job)
        {
            _generator.PositionChanged(job.Job);
        }
    }
}
