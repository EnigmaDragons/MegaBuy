using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    // @todo #1 When a new JobRole is accepted, the player should receive a notification
    public class JobRoleAccepted
    {
        public JobRole JobRole { get; }

        public JobRoleAccepted(JobRole jobRole)
        {
            JobRole = jobRole;
        }
    }
}
