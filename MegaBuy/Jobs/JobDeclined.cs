namespace MegaBuy.Jobs
{
    public class JobDeclined
    {
        public Job Job { get; }

        public JobDeclined(Job job)
        {
            Job = job;
        }
    }
}
