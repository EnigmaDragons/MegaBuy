using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class JobRoleOffered
    {
        public Job Job { get; }
        public string Message { get; }

        public JobRoleOffered(string message, Job job)
        {
            Message = message;
            Job = job;
        }
    }
}