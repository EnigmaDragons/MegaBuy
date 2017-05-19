using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories.Data
{
    public static class PromoCodes
    {
        private static readonly List<string> Codes = new List<string>
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "CYBERMONDAYSPECIAL",
            "LOVE20",
            "TAKE8MAY17",
            "DEMONS",
            "PRAISETHESUN",
            "PROMOWIN4",
            "IRLYLVMEGABUY",
        };

        public static string Random => Codes.Random();
    }
}
