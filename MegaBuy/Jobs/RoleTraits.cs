using System.Collections.Generic;
using MegaBuy.Jobs.Referrer;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using MegaBuy.Policies;

namespace MegaBuy.Jobs
{
    public static class RoleTraits
    {
        public static Dictionary<JobRole, List<Policy>> Policies = new Dictionary<JobRole, List<Policy>>
        {
            { JobRole.ReferrerLevel1, ReferrerPolicies.Level1Policies },
            { JobRole.ReferrerLevel2, ReferrerPolicies.Level2Policies },
            { JobRole.ReferrerLevel3, ReferrerPolicies.Level3Policies },
        };

        public static Dictionary<JobRole, PerCallRate> Rates = new Dictionary<JobRole, PerCallRate>
        {
            { JobRole.ReferrerLevel1, ReferrerPerCallRates.Level1PerCallRate },
            { JobRole.ReferrerLevel2, ReferrerPerCallRates.Level2PerCallRate },
            { JobRole.ReferrerLevel3, ReferrerPerCallRates.Level3PerCallRate },
        };
    }
}
