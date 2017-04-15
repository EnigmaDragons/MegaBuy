using MegaBuy.Money;
using MegaBuy.Money.Amounts;
using MegaBuy.Policies;

namespace MegaBuy.Calls.Events
{
    public struct TechnicalMistakeOccurred
    {
        public IAmount PayPenalty { get; }
        public Policy Policy { get; }

        public TechnicalMistakeOccurred(IAmount parPenalty, Policy policy)
        {
            PayPenalty = parPenalty;
            Policy = policy;
        }
    }
}
