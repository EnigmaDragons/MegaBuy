using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MegaBuy.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.MegaBuyCorporation.JobRoles.Referrer
{
    public static class ReferrerPolicies
    {
        private static readonly Predicate<Caller> Any = x => true;
        private static readonly Predicate<Caller> HasOpenOrder = x => x.TraitMatches("OpenOrder", "true");
        private static readonly Predicate<Caller> IsCustomer = x => x.TraitMatches("IsCustomer", "true");

        public static readonly List<Policy> Level1Policies = new List<Policy>()
        {
            new Policy("Any caller may be referred to Info", CallResolution.Any, x => true),
            new Policy("Customers may be referred to Troubleshooting", CallResolution.ReferToTroubleshooting, IsCustomer),
            new Policy("Customers may be referred to Returns", CallResolution.ReferToReturns, IsCustomer),
            new Policy("If a caller has another issue, escalate them", CallResolution.Any, x => true),
        };

        public static readonly List<Policy> Level2Policies = new List<Policy>()
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
