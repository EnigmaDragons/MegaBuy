
using MegaBuy.Money;
using MegaBuy.Money.Amounts;

namespace MegaBuy.Calls.Events
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
