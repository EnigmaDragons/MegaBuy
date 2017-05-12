using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Money.Amounts;

namespace MegaBuy.Calls.Events
{
    public class TechnicalMistakeOccurred
    {
        public IAmount PayPenalty { get; }
        public Policy Policy { get; }

        public TechnicalMistakeOccurred(IAmount payPenalty, Policy policy)
        {
            PayPenalty = payPenalty;
            Policy = policy;
        }
    }
}
