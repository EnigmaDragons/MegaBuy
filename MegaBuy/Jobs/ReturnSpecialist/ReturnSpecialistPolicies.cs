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

        private static readonly Predicate<Caller> Any = x => true;
        private static readonly Predicate<Caller> ItemWasPurchasedAtMegaBuy = x => x.TraitMatches("ItemWasPurchasedAt", "MegaBuy");
        private static readonly Predicate<Caller> ItemIsBrokenAndIsWithin60Days = x => x.HasTrait("ItemIsBroken")
            && x.IsAtMost("ItemWasDeliveredAt", 60);
        private static readonly Predicate<Caller> ItemIsNotBrokenAndIsWithin30Days = x => !x.HasTrait("ItemIsBroken")
            && x.IsAtMost("ItemWasDeliveredAt", 30);
        private static readonly Predicate<Caller> ReplacementIsInStock = x => x.HasTrait("ItemIsReplacable");
        private static readonly Predicate<Caller> ItemNotSoldAsIs = x => !x.HasTrait("ItemWasSoldAsIs");
        
        public static List<Policy> Level1 = new List<Policy>
        {
            new Policy("Returns must have been purchased at MegaBuy", ItemWasPurchasedAtMegaBuy, ReturnOrReplace),
            new Policy("Broken items may be returned within 60 days of delivery", ItemIsBrokenAndIsWithin60Days, ReturnOrReplace),
            new Policy("Other returns must happen within 30 days of delivery", ItemIsNotBrokenAndIsWithin30Days, ReturnOrReplace),
            new Policy("Any item which may be returned, may instead be replaced", Any, ReturnOrReplace),
            new Policy("No item may be replaced, if the item is not in stock", ReplacementIsInStock, Replace),
            new Policy("Items sold \"As-Is\" may not returned", ItemNotSoldAsIs, ReturnOrReplace),
        };

        // @todo #1 Create ReturnSpecialist Level 2 policies
    }
}
