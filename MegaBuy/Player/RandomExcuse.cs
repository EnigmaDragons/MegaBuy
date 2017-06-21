using System.Collections.Generic;
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
