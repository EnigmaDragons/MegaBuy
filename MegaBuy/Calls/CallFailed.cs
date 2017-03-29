
using MegaBuy.Money;

namespace MegaBuy.Calls
{
    public struct CallFailed
    {
        public IAmount PayPenalty { get; }

        public CallFailed(IAmount amount)
        {
            PayPenalty = amount;
        }
    }
}
