using System;
using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MegaBuy.MegaBuyCorporation.Policies;

namespace MegaBuy.Jobs.ReturnSpecialist
{
    public static class ReturnSpecialistPolicies
    {
        private static readonly CallResolution[] ReturnOrReplace = {CallResolution.ApproveReplacement, CallResolution.ApproveReturn};
        private const CallResolution Replace = CallResolution.ApproveReplacement;

        // @todo #1 Backend: Change these Caller Predicates to Product or Call Predicates
        private static readonly Predicate<Caller> Any = x => true;
        private static readonly Predicate<Caller> ItemWasPurchasedAtMegaBuy = x => x.TraitMatches("ItemWasPurchasedAt", "MegaBuy");
        private static readonly Predicate<Caller> ItemIsBrokenAndIsWithin60Days = x => x.HasTrait("ItemIsBroken")
            && x.IsAtMost("ItemWasDeliveredAt", 60);
        private static readonly Predicate<Caller> ItemIsNotBrokenAndIsWithin30Days = x => !x.HasTrait("ItemIsBroken")
            && x.IsAtMost("ItemWasDeliveredAt", 30);
        private static readonly Predicate<Caller> ReplacementIsInStock = x => x.HasTrait("ItemIsReplacable");
        private static readonly Predicate<Caller> ItemNotSoldAsIs = x => !x.HasTrait("ItemWasSoldAsIs");

        // @todo #1 Backend: Plug in logic for Level 2 predicates
        private static readonly Predicate<Caller> ItemWasDelivered = x => true;
        private static readonly Predicate<Caller> IfSoftwareIsWithin15Days = x => true;
        private static readonly Predicate<Caller> ItemCostsLessThanFiftyThousand = x => true;
        private static readonly Predicate<Caller> IfWeaponHasShippingAddress = x => true;

        public static List<Policy> Level1 = new List<Policy>
        {
            new Policy("Returns must have been purchased at MegaBuy", ItemWasPurchasedAtMegaBuy, ReturnOrReplace),
            new Policy("Broken items may be returned within 60 days of delivery", ItemIsBrokenAndIsWithin60Days, ReturnOrReplace),
            new Policy("Other returns must happen within 30 days of delivery", ItemIsNotBrokenAndIsWithin30Days, ReturnOrReplace),
            new Policy("Any item which may be returned, may instead be replaced", Any, ReturnOrReplace),
            new Policy("No item may be replaced, if the item is not in stock", ReplacementIsInStock, Replace),
            new Policy("Items sold \"As-Is\" may not returned", ItemNotSoldAsIs, ReturnOrReplace),
        };

        public static List<Policy> Level2 = new List<Policy>
        {
            new Policy("Items may not be returned until after they have been delivered", ItemWasDelivered, ReturnOrReplace),
            new Policy("Software products must be returned within 15 days", IfSoftwareIsWithin15Days, ReturnOrReplace),
            new Policy("Items costing over $50000 may not be returned", ItemCostsLessThanFiftyThousand, ReturnOrReplace),
            new Policy("Weapons may not be returned without a recorded Shipping Address", IfWeaponHasShippingAddress, ReturnOrReplace),
        };
    }
}
