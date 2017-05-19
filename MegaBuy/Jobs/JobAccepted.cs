using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    // @todo #1 When a new Job is accepted, the player should receive a notification
    public class JobAccepted
    {
        public Job Job { get; }

        public JobAccepted(Job job)
        {
            Job = job;
        }
    }
}
