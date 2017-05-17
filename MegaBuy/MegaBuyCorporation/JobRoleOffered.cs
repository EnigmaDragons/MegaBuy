using MegaBuy.Calls.Rules;
using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class JobRoleOffered
    {
        public JobRole JobRole { get; }
        public string Message { get; }

        public JobRoleOffered(string message, JobRole jobRole)
        {
            Message = message;
            JobRole = jobRole;
        }
    }
}