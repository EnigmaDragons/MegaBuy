using System;
using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MegaBuy.Policies;

namespace MegaBuy.JobRoles.Referrer
{
    public static class ReferrerLevel1Policies
    {
        private static readonly Predicate<Caller> IsCustomer = x => x.TraitMatches("IsCustomer", "true");

        public static readonly List<Policy> Policies = new List<Policy>()
        {
            new Policy("Any caller may be referred to Info", CallResolution.Any, x => true),
            new Policy("Customers may be referred to Troubleshooting", CallResolution.ReferToTroubleshooting, IsCustomer),
            new Policy("Customers may be referred to Returns", CallResolution.ReferToReturns, IsCustomer),
            new Policy("If a caller has another issue, escalate them", CallResolution.Any, x => true),
        };
    }
}
