using MegaBuy.Calls.Rules;

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
