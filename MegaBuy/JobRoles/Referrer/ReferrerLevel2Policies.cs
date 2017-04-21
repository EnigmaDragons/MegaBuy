using System;
using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MegaBuy.Policies;

namespace MegaBuy.JobRoles.Referrer
{
    public static class ReferrerLevel2Policies
    {
        private static readonly Predicate<Caller> Any = x => true;
        private static readonly Predicate<Caller> IsCustomer = x => x.TraitMatches("IsCustomer", "true");
        private static readonly Predicate<Caller> HasOpenOrder = x => x.TraitMatches("OpenOrder", "true");

        public static readonly List<Policy> Policies = new List<Policy>()
        {
            new Policy("Any caller may be referred to Info", CallResolution.Any, Any),
            new Policy("Customers may be referred to Troubleshooting", CallResolution.ReferToTroubleshooting, IsCustomer),
            new Policy("Customers may be referred to Returns", CallResolution.ReferToReturns, IsCustomer),
            new Policy("Any caller may be referred to Careers", CallResolution.ReferToCareers, Any),
            new Policy("Customers with undelivered purchases may be referred to Orders", CallResolution.ReferToOrders, HasOpenOrder),
            new Policy("If a caller has another issue, escalate them", CallResolution.Any, Any),
        };
    }
}
