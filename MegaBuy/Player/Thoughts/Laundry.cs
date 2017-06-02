using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Player.Thoughts
{
    public static class Laundry
    {
        private static readonly List<string> _thoughts = new List<string>
        {
            "Hmm, this laundry in the middle of the floor sure does remind me of good times in college.",
            "Nothing like a little splash of color!",
            "Someday I need to get a red shirt!",
            "At least my socks won't disappear while I can see them.",
            "I love blue jeans. So comfortable!",
            "At least I'll have something to wear on St. Patricks day.",
            "I better not move these, I'll just have to get them out later.",
            "I'll never understand why they sell socks in singles, I always have to buy at least two!",
            "I hate doing my laundry, I'm just going to leave them here.",
            "I should go make some more money..."
        };
        
        public static string GetThought()
        {
            return _thoughts.Random();
        }
    }
}
