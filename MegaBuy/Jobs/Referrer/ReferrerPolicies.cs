using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MegaBuy.MegaBuyCorporation.Policies;

namespace MegaBuy.Jobs.Referrer
{
    public static class ReferrerPolicies
    {
        private static readonly Predicate<Call> Any = x => true;
        private static readonly Predicate<Call> HasOpenOrder = x => x.Caller.HasTrait("OpenOrder");
        private static readonly Predicate<Call> IsCustomer = x => x.Caller.HasTrait("IsCustomer");
        private static readonly Predicate<Call> IsCallingAboutPurchase = x => x.Caller.HasTrait("IsCallingAboutPurchase");
        private static readonly Predicate<Call> IsSupplierEmployee = x => x.Caller.HasTrait("IsSupplierEmployee");
        private static readonly Func<Call, int, bool> HasPurchasedInLastXDays = (c, x) => c.Caller.IsAtMost("MostRecentPurchaseTimeIs", x);
        private static readonly Func<Call, int, bool> HasCreditScoreAtLeastX = (c, x) => c.Caller.IsAtLeast("CreditScoreIs", x);

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
            new Policy("Customers with a purchase in the last 16 days may be referred to Returns", CallResolution.ReferToReturns,
                (c) => HasPurchasedInLastXDays(c, 16)),
            new Policy("Any caller with a resume may be referred to Careers", CallResolution.ReferToCareers, Any),
            new Policy("Customers with purchases in the last 20 days may be referred to Orders", CallResolution.ReferToOrders,
                (c) => HasPurchasedInLastXDays(c, 20)), 
            new Policy("Any caller may be referred to Legal", CallResolution.ReferToLegal, Any),
            new Policy("Employees of MegaBuy suppliers may be referred to Accounting", CallResolution.ReferToAccounting, IsSupplierEmployee),
            new Policy("Any caller with a Credit Score of 11000+ may be referred to Recommendations", CallResolution.ReferToRecommendations,
                (c) => HasCreditScoreAtLeastX(c, 11000)),
            new Policy("Any caller may be referred to Feedback", CallResolution.ReferToFeedback, Any),
            new Policy("If a caller has another issue, refer them to a Generalist", CallResolution.ReferToGeneralist, Any),
            new Policy("Any caller may be referred to Info", CallResolution.Any, Any),
            new Policy("Customers may be referred to Troubleshooting for a product purchased from MegaBuy",
                CallResolution.ReferToTroubleshooting, IsCallingAboutPurchase),
            new Policy("Customers with a purchase in the last 16 days may be referred to Returns", CallResolution.ReferToReturns,
                (c) => HasPurchasedInLastXDays(c, 16)),
            new Policy("Any caller with a resume may be referred to Careers", CallResolution.ReferToCareers, Any),
            new Policy("Customers with purchases in the last 20 days may be referred to Orders", CallResolution.ReferToOrders,
                (c) => HasPurchasedInLastXDays(c, 20)),
            new Policy("Any caller may be referred to Legal", CallResolution.ReferToLegal, Any),
            new Policy("Employees of MegaBuy suppliers may be referred to Accounting", CallResolution.ReferToAccounting, IsSupplierEmployee),
            new Policy("Any caller with a Credit Score of 11000+ may be referred to Recommendations", CallResolution.ReferToRecommendations,
                (c) => HasCreditScoreAtLeastX(c, 11000)),
            new Policy("Any caller may be referred to Feedback", CallResolution.ReferToFeedback, Any),
            new Policy("If a caller has another issue, refer them to a Generalist", CallResolution.ReferToGeneralist, Any),
            new Policy("Any caller may be referred to Info", CallResolution.Any, Any),
            new Policy("Customers may be referred to Troubleshooting for a product purchased from MegaBuy",
                CallResolution.ReferToTroubleshooting, IsCallingAboutPurchase),
            new Policy("Customers with a purchase in the last 16 days may be referred to Returns", CallResolution.ReferToReturns,
                (c) => HasPurchasedInLastXDays(c, 16)),
            new Policy("Any caller with a resume may be referred to Careers", CallResolution.ReferToCareers, Any),
            new Policy("Customers with purchases in the last 20 days may be referred to Orders", CallResolution.ReferToOrders,
                (c) => HasPurchasedInLastXDays(c, 20)),
            new Policy("Any caller may be referred to Legal", CallResolution.ReferToLegal, Any),
            new Policy("Employees of MegaBuy suppliers may be referred to Accounting", CallResolution.ReferToAccounting, IsSupplierEmployee),
            new Policy("Any caller with a Credit Score of 11000+ may be referred to Recommendations", CallResolution.ReferToRecommendations,
                (c) => HasCreditScoreAtLeastX(c, 11000)),
            new Policy("Any caller may be referred to Feedback", CallResolution.ReferToFeedback, Any),
            new Policy("If a caller has another issue, refer them to a Generalist", CallResolution.ReferToGeneralist, Any),
        };
    }
}
