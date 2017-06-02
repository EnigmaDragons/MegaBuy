using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Player.Thoughts
{
    public static class ComputerRig
    {
        private static readonly List<string> _thoughts = new List<string>
        {
            "I love my green alien glow theme!",
            "Home sweet home.",
            "I can't imagine life without my computer.",
            "I wonder what new stuff has been posted...",
            "My Tri-Penta XL processor is so sweet!",
            "I wish I could upgrade to the new 180 FPS monitors...",
            "One day I'm going to get a faux leather chair...",
            "My kill streak last week was so badass!",
            "Some day I really should sort my 10,384,211 files.",
            "Bah! Who needs backups..."
        };

        public static string GetThought()
        {
            return _thoughts.Random();
        }
    }
}
