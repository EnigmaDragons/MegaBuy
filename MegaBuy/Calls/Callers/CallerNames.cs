﻿using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Callers
{
    public class CallerNames
    {
        // @todo #1 Add 5 new callers (names + art)
        private static string[] names = 
        {
            "Chaos Theory",
            "Noise",
            "Dalton Bowdoin",
            "Shade Corwin",
            "Shad Holbach",
            "Jonas Rhyne",
            "Darrell Riordan",
            "Anelle Ryen",
            "Heidy Slayton",
            "Lilliam Watrous",
            "Lorilee Faulken",
            "Liniya Kupshcheva",
            "Gemma Hunter",
            "Ash Jacobson",
            "Mark Dennison",
            "Serra Rhonas",
            "Eli Daniels",

        };

        public static string Random => names.Random();
    }
}
