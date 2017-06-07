using System.Collections.Generic;
using MonoDragons.Core.Common;


namespace MegaBuy.Player.Thoughts
{
    public static class BottleBox
    {
        private static readonly List<string> _thoughts = new List<string>
        {
            "I wonder if water is even good for you.",
            "My great-great-grandfather died after drinking water. I definitely don't want to do that.",
            "Why do I even have this water.",
            "Water has been used to murder people. I don't want to drink a murder weapon.",
            "Who invented water?",
            "Hydration is a hoax.",
            "I should throw these bottles away some year. But that sounds like way too much work.",
            "Maybe if I drunk the water I could use these bottles for something useful.",
            "If I had a bottle of water for every job I've had, I'd have 4 bottles. And I have 4 bottles.",
            "If water is so good for you, then why does everybody who drinks it die?"
        };

        public static string GetThought()
        {
            return _thoughts.Random();
        }
    }
}
