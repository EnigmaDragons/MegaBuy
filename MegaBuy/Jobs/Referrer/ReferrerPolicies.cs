using System;
using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MegaBuy.Policies;

namespace MegaBuy.Jobs.Referrer
{
    public static class ReferrerPolicies
    {
        private static readonly Predicate<Caller> Any = x => true;
        private static readonly Predicate<Caller> HasOpenOrder = x => x.TraitMatches("OpenOrder", "true");
        private static readonly Predicate<Caller> IsCustomer = x => x.TraitMatches("IsCustomer", "true");
        private static readonly Predicate<Caller> IsCallingAboutPurchase = x => x.TraitMatches("IsCallingAboutPurchase", "true");
        private static readonly Predicate<Caller> IsSupplierEmployee = x => x.TraitMatches("IsSupplierEmployee", "true");

        public static readonly List<Policy> Level1Policies = new List<Policy>
        {
            new Policy("Any caller may be referred to Info", CallResolution.Any, x => true),
            new Policy("Customers may be referred to Troubleshooting", CallResolution.ReferToTroubleshooting, IsCustomer),
            new Policy("Customers may be referred to Returns", CallResolution.ReferToReturns, IsCustomer),
            new Policy("If a caller has another issue, escalate them", CallResolution.Any, x => true),
        };

        public static readonly List<Policy> Level2Policies = new List<Policy>
        {
            new Policy("Any caller may be referred to Info", CallResolution.Any, Any),
            new Policy("Customers may be referred to Troubleshooting", CallResolution.ReferToTroubleshooting, IsCustomer),
            new Policy("Customers may be referred to Returns", CallResolution.ReferToReturns, IsCustomer),
            new Policy("Any caller may be referred to Careers", CallResolution.ReferToCareers, Any),
            new Policy("Customers with undelivered purchases may be referred to Orders", CallResolution.ReferToOrders, HasOpenOrder),
            new Policy("If a caller has another issue, escalate them", CallResolution.Any, Any),
        };

        public static readonly List<Policy> Level3Policies = new List<Policy>
        {
            new Policy("Any caller may be referred to Info", CallResolution.Any, Any),
            new Policy("Customers may be referred to Troubleshooting for a product purchased from MegaBuy",
                CallResolution.ReferToTroubleshooting, IsCallingAboutPurchase), 
            // @todo #1 Create and wire in Last X Days purchase history condition
            new Policy("Customers with a purchase in the last 16 days may be referred to Returns", CallResolution.ReferToReturns, Any),
            new Policy("Any caller with a resume may be referred to Careers", CallResolution.ReferToCareers, Any),
            // Update once Last X Days purchase history condition is created
            new Policy("Customers with purchases in the last 20 days may be referred to Orders", CallResolution.ReferToOrders, Any), 
            new Policy("Any caller may be referred to Legal", CallResolution.ReferToLegal, Any),
            new Policy("Employees of MegaBuy suppliers may be referred to Accounting", CallResolution.ReferToAccounting, IsSupplierEmployee),
            // @todo #1 Create and wire in Credit Score condition
            new Policy("Any caller with a Credit Score of 11000+ may be referred to Recommendations", CallResolution.ReferToRecommendations, Any),
            new Policy("Any caller may be referred to Feedback", CallResolution.ReferToFeedback, Any),
            new Policy("If a caller has another issue, refer them to a Generalist", CallResolution.ReferToGeneralist, Any),
        };
    }
}
