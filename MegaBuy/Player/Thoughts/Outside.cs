﻿using MonoDragons.Core.Common;

namespace MegaBuy.Player.Thoughts
{
    public static class Outside
    {
        private static readonly string[] _thoughts =
        {
            "Fuck outside!",
            "I'm too tired to go outside.",
            "There's scary things out there! I'm not going out there!",
            "Nope!",
            "Are those sirens?? No, thank you.",
            "Last time I went outside, a homeless person talked to me. No way I'm taking that risk again!",
            "Expensive things are out there. I don't much care about being reminded of all the things I'll never have.",
            "Isn't there some TV to watch?",
            "Maybe I should take a shower instead..."
        };

        public static string GetThought()
        {
            return _thoughts.Random();
        }
    }
}