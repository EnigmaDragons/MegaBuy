
namespace MegaBuy.Jobs
{
    public class JobChanged
    {
        public Job Job { get; }

        public JobChanged(Job job)
        {
            Job = job;
        }
    }
}
