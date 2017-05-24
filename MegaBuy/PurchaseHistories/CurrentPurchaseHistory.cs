using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.PurchaseHistories
{
    public class CurrentPurchaseHistory
    {
        public static IEnumerable<Purchase> PurchaseHistory { get; set; }
    }
}
