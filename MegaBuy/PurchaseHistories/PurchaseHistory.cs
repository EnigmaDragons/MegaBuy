using System.Collections.Generic;
using System.Linq;

namespace MegaBuy.PurchaseHistories
{
    public sealed class PurchaseHistory
    {
        public IEnumerable<Purchase> Purchases { get; private set; }

        public PurchaseHistory()
            : this(Purchase.CreateInfinite()) { }

        public PurchaseHistory(Purchase purchase)
            : this (Purchase.CreateInfiniteWith(purchase)) { }

        private PurchaseHistory(IEnumerable<Purchase> purchases)
        {
            Purchases = purchases.Take(150).ToList();
        }
    }
}
