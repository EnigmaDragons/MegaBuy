using MegaBuy.Calls.Rules;
using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class PromotionAccepted
    {
        public JobRole JobRole { get; }

        public PromotionAccepted(JobRole jobRole)
        {
            JobRole = jobRole;
        }
    }
}
