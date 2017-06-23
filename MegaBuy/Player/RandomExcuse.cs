using System.Collections.Generic;
using MegaBuy.PurchaseHistories.Data;
using MonoDragons.Core.Common;

namespace MegaBuy.Player
{
    public sealed class RandomExcuse
    {
        private static readonly List<string> Excuses = new List<string>
        {
            "Hold on. I'm looking up your order.",
            "Hmmmm... this looks unusual.",
            "Everything seems great so far. I have to check one more thing.",
            "This company policy isn't entirely clear. Give me a moment.",
            "I am nearly finished.",
            "Things are looking very good for your request.",
            "I didn't know we still sold this product.",
            "Ughhh... All these nulls in the database.",
            "Hang on.",
            "I'm now filling out a requisition form.",
            "The tax forms are going to take me a minute.",
            "I'm surprised you bought from this provider.",
            "Please be patient. I'm working as fast as I can.",
            "Uh-oh. I tapped on the wrong screen.",
            "Is my face not showing up for you?.",
            "I wish our devs would fix this damn video chat.",
            "I'm just nine easy steps away from finished.",
            "I apologize. My food order just arrived.",
            $"Shipped to {ShippingAddresses.Random}?",
            "Hold on. It's loading...",
        };

        private readonly string _excuse;

        public RandomExcuse()
        {
            _excuse = Excuses.Random();
        }

        public static implicit operator string(RandomExcuse excuse)
        {
            return excuse._excuse;
        }
    }
}
