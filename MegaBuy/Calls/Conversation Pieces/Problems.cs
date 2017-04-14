using System.Collections.Generic;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class Verbs
    {
        public static Dictionary<NounOperations, string> verbs = new Dictionary<NounOperations, string>
        {
            { NounOperations.CanRun, "run" },
            { NounOperations.CanTurnOnOrOff, "turn on" },
            { NounOperations.CanTurnOnOrOff, "turn off" },
            { NounOperations.Jams, "always jams" },
            { NounOperations.IsDefective, "arrived with a defect" },
            { NounOperations.IsBuggy, "has tons of known bugs" },
            { NounOperations.HasSpyware, "infected my system with spyware" },
            { NounOperations.RunsSlowly, "infected my system with spyware" },
        };

        public static string[] canNotSynonyms = new string[] { "can't", "fails to", "is unable to" };
    }

    public enum NounOperations
    {
        CanRun,
        CanTurnOnOrOff,
        Jams,
        IsDefective,
        TerribleExperience,
        IsBuggy,
        HasSpyware,
        RunsSlowly
    }
}
