using MegaBuy.Money.Amounts;

namespace MegaBuy.MegaBuyCorporation.JobRoles.Referrer
{
    public class PerCallRate : IAmount
    {
        private decimal v;

        public PerCallRate(decimal v)
        {
            this.v = v;
        }

        public decimal Amount()
        {
            return v;
        }
    }
}