﻿using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Rules;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.PurchaseHistories.Data;

namespace MegaBuy.Jobs.ReturnSpecialist
{
    public static class ReturnSpecialistPolicies
    {
        private static readonly CallResolution[] ReturnOrReplace = {CallResolution.ApproveReplacement, CallResolution.ApproveReturn};
        private const CallResolution Replace = CallResolution.ApproveReplacement;
        
        private static readonly Predicate<Call> Any = x => true;
        private static readonly Predicate<Call> ItemWasPurchasedAtMegaBuy = x => x.Purchase.HasValue;
        // @todo #1 Backend: Design logic for checking broken
        private static readonly Predicate<Call> ItemIsBrokenAndIsWithin60Days = x => x.Purchase.IsTrue(y => y.PurchasedWithinLast(60));
        private static readonly Predicate<Call> ItemIsNotBrokenAndIsWithin30Days = x => x.Purchase.IsTrue(y => y.PurchasedWithinLast(30));
        private static readonly Predicate<Call> ReplacementIsInStock = x => x.Scenario.NumInStock > 0;
        private static readonly Predicate<Call> ItemNotSoldAsIs = x => x.Purchase.IsFalse(y => y.SoldAsIs);
        
        private static readonly Predicate<Call> ItemWasDelivered = x => x.Purchase.IsTrue(y => y.IsDelivered);
        private static readonly Predicate<Call> IfSoftwareIsWithin15Days = 
            x => x.Purchase.IsTrue(y => y.ProductCategory == ProductCategory.Software.ToString() && y.PurchasedWithinLast(15));
        private static readonly Predicate<Call> ItemCostsLessThanFiftyThousand = x => x.Purchase.IsTrue(y => y.PriceIsLessThan(50000));
        private static readonly Predicate<Call> IfWeaponHasShippingAddress = 
            x => x.Purchase.IsTrue(y => y.ProductCategory == ProductCategory.Weapon.ToString() && y.ShippingAddress != "NULL");

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
