using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Player
{
    public sealed class RandomExcuse
    {
        private static readonly List<string> Excuses = new List<string>
        {
            "Hold on. I'm looking up your order."
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
