using MegaBuy.Calls.Rules;
using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class PromotionDeclined
    {
        public JobRole JobRole { get; }

        public PromotionDeclined(JobRole jobRole)
        {
            JobRole = jobRole;
        }
    }
}
