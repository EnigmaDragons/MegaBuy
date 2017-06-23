using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public sealed class RandomComplaint
    {
        private static readonly List<string> Complaints = new List<string>
        {
            "Are you still there?",
            "What's taking so long?",
            "These calls always take so long. I hate calling.",
            "I really regret my purchase.",
            "What's happening?",
            "Are you going to be able to help me?",
            "How are things looking?",
            "I can mail it to you right away.",
            "Are we good?",
            "*Sigh*",
        };

        private readonly string _complaint;

        public RandomComplaint()
        {
            _complaint = Complaints.Random();
        }

        public static implicit operator string(RandomComplaint complaint)
        {
            return complaint._complaint;
        }
    }
}
