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
            "Do you need any more information from me?",
            "Listen, I'm in a hurry today!",
            "Please help me out. Payday is so far away.",
            "I didn't expect it to be this much trouble.",
            "Are these calls always so slow?",
            "What's the deal?",
            "This has been a major inconvenience! Can you hook me up?",
            "I hope not all the support staff is this slow.",
            "Isn't MegaBuy supposed to have the best customer service?",
            "I don't have all day.",
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
