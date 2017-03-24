using MegaBuy.Money;

namespace MegaBuy.Calls
{
    public struct TechnicalMistakeOccurred
    {
        public IAmount PayPenalty { get; }

        public TechnicalMistakeOccurred(IAmount parPenalty)
        {
            PayPenalty = parPenalty;
        }
    }
}
