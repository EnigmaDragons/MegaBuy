using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;

namespace MegaBuy.Jobs.Referrer
{
    public static class ReferrerPerCallRates
    {
        public static readonly PerCallRate Level1PerCallRate = new PerCallRate(2);
        public static readonly PerCallRate Level2PerCallRate = new PerCallRate(100);
    }
}
