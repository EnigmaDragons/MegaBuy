using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories.Data
{
    public static class AddressOwners
    {
        public static string Random => Owners.Random();

        private static readonly List<string> Owners = new List<string>
        {
            "Mobil Systems Inc",
            "MORGAN JOHNSON MARKETING ASSOCIATES",
            "Oates Flag Co",
            "Covert Architecture",
            "Machina Dynamica Inc",
            "Octagon Marketing",
            "R J Lee Group Inc",
            "Arden & Assoc",
            "Jeff Bienkowski",
            "The Capital Financial Group",
            "World Trade Enterprises",
            "Greens Of Bedford",
            "Canterbury Lane Apts",
            "Bid Wellnalley Inc.",
            "Citadel Apartments",
            "Sarasota Foreclosures Com Inc",
            "The Falck Company",
        };
    }
}
