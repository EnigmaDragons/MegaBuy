using MonoDragons.Core.Common;

namespace MegaBuy.Player.Thoughts
{
    public sealed class Shower
    {
        private readonly string[] _thoughts =
        {
            "Fuck showers!",
            "I'm way too lazy to bother with cleanliness...",
            "Last time I took a shower, I almost slipped and died.",
            "Not enough time for that crap.",
            "Nope!"
        };

        public string GetThought()
        {
            return _thoughts[Rng.Int(0, _thoughts.Length)];
        }
    }
}