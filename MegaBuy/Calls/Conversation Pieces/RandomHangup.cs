using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class RandomHangup
    {
        private static readonly List<string> Hangups = new List<string>
        {
            "Fucking customer support!",
            "This isn't worth it!",
            "Fuck you!",
            "I hope you get fired!",
            "Peace out you worthless human."
        };

        private readonly string _hangup;

        public RandomHangup()
        {
            _hangup = Hangups.Random();
        }

        public static implicit operator string(RandomHangup hangup)
        {
            return hangup._hangup;
        }
    }
}
