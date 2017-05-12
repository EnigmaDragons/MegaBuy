using MegaBuy.Calls.Rules;
using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public struct PromotionOffered
    {
        public JobRole JobRole { get; }
        public string Message { get; }

        public PromotionOffered(string message, JobRole jobRole)
        {
            Message = message;
            JobRole = jobRole;
        }
    }
}