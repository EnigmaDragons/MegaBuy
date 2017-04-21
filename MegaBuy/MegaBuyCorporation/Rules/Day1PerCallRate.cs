
using MegaBuy.Money.Amounts;

namespace MegaBuy.Money.Rules
{
    public class Day1PerCallRate : IPerCallRate
    {
        public decimal Amount()
        {
            return 2;
        }
    }
}
