using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories.Data
{
    public static class Providers
    {
        public static Provider Random
        {
            get
            {
                var name = ProviderNames.Random();
                return new Provider
                {
                    Name = name,
                    Id = GetProviderId(name)
                };
            }
        }

        private static string GetProviderId(string provider)
        {
            if (!ProviderIds.ContainsKey(provider))
                ProviderIds[provider] = Rng.Int();
            return ProviderIds[provider].ToString();
        }

        private static readonly Dictionary<string, int> ProviderIds = new Dictionary<string, int>();

        private static readonly List<string> ProviderNames = new List<string>
        {
            "Faxla",
            "Pfannenberg Inc",
            "Digital Paradigm Solutions",
            "Panserv",
            "Versametrix",
            "Prodecom",
            "Memories Preserved Enterprises",
            "Fiserv Inc",
            "John Shipinski",
            "Indotronix International Corp",
            "Hyde Co",
            "Benton Newton And Partners",
            "684 5th Ave",
            "Progressive Methods Inc",
            "Domer Marketing",
            "Karaoke Bookings & Studios",
            "Wizware Incorporated",
        };
    }
}
