using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Jobs.Referrer;
using MegaBuy.Jobs.ReturnSpecialist;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using MegaBuy.MegaBuyCorporation.Policies;

namespace MegaBuy.Jobs
{
    public static class JobTraits
    {
        public static Dictionary<Job, List<Policy>> Policies = new Dictionary<Job, List<Policy>>
        {
            { Job.ReferrerLevel1, ReferrerPolicies.Level1Policies },
            { Job.ReferrerLevel2, ReferrerPolicies.Level2Policies },
            { Job.ReferrerLevel3, ReferrerPolicies.Level3Policies },
            { Job.ReturnSpecialistLevel1, ReturnSpecialistPolicies.Level1 },
        };

        public static Dictionary<Job, PerCallRate> Rates = new Dictionary<Job, PerCallRate>
        {
            { Job.ReferrerLevel1, ReferrerPerCallRates.Level1PerCallRate },
            { Job.ReferrerLevel2, ReferrerPerCallRates.Level2PerCallRate },
            { Job.ReferrerLevel3, ReferrerPerCallRates.Level3PerCallRate },
            { Job.ReturnSpecialistLevel1, ReturnSpecialistPerCallRates.Level1PerCallRate },
        };

        public static Dictionary<Job, IVisualControl> Controls = new Dictionary<Job, IVisualControl>
        {
            { Job.ReferrerLevel1, new ReferrerUI() },
            { Job.ReferrerLevel2, new ReferrerUI() },
            { Job.ReferrerLevel3, new ReferrerUI() },
            { Job.ReturnSpecialistLevel1, new ReturnSpecialistUI() },
        };

        public static Dictionary<Job, Func<Call>> Calls = new Dictionary<Job, Func<Call>>
        {
            { Job.ReferrerLevel1, () => ReferrerCalls.NewLevel1Call() },
            { Job.ReferrerLevel2, () => ReferrerCalls.NewLevel2Call() },
            { Job.ReferrerLevel3, () => ReferrerCalls.NewLevel3Call() },
            { Job.ReturnSpecialistLevel1, () => ReturnSpecialistCalls.NewLevel1Call() },
        };
    }
}
