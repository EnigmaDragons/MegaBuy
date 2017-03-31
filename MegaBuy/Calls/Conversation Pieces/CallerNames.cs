using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class CallerNames
    {
        public static string[] names = { "Chaos Theory", "Noise" };
        public static string GetRandomName()
        {
            return names[Rng.Int(CallerNames.names.Length)];
        }
    }
}
