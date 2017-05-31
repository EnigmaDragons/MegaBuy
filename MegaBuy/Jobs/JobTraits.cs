using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Jobs.ReturnSpecialist;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using MegaBuy.MegaBuyCorporation.Policies;

namespace MegaBuy.Jobs
{
    public static class JobTraits
    {
        public static Dictionary<Job, List<Policy>> Policies = new Dictionary<Job, List<Policy>>
        {
            { Job.ReturnSpecialistLevel1, ReturnSpecialistPolicies.Level1 },
        };

        public static Dictionary<Job, PerCallRate> Rates = new Dictionary<Job, PerCallRate>
        {
            { Job.ReturnSpecialistLevel1, ReturnSpecialistPerCallRates.Level1PerCallRate },
        };

        public static Dictionary<Job, IVisualControl> Controls = new Dictionary<Job, IVisualControl>
        {
            { Job.ReturnSpecialistLevel1, new ReturnSpecialistUI() },
        };

        public static Dictionary<Job, Func<Call>> Calls = new Dictionary<Job, Func<Call>>
        {
            { Job.ReturnSpecialistLevel1, () => ReturnSpecialistCalls.NewLevel1Call() },
        };
    }
}
