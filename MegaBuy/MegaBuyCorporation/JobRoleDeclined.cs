using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class JobRoleDeclined
    {
        public JobRole JobRole { get; }

        public JobRoleDeclined(JobRole jobRole)
        {
            JobRole = jobRole;
        }
    }
}
