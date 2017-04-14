﻿using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class CallerNames
    {
        private static string[] names = 
        {
            "Chaos Theory",
            "Noise",
            "Dalton Bowdoin",
            "Shade Corwin",
            "Shad Holbach",
            "Jonas Rhyne",
            "Darrell Riordan"
        };

        public static string Random()
        {
            return names.Random();
        }
    }
}
