using System.Collections.Generic;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class Problems
    {
        public static Dictionary<Problem, string> Description = new Dictionary<Problem, string>
        {
            { Problem.DoesntRun, "can't run" },
            { Problem.DoesntTurnOn, "doesn't turn on" },
            { Problem.Jams, "always jams" },
            { Problem.IsDefective, "arrived with a defect" },
            { Problem.TerribleExperience, "is the worst" },
            { Problem.IsBuggy, "has tons of known bugs" },
            { Problem.HasSpyware, "infected my system with spyware" },
            { Problem.RunsSlowly, "takes forever to launch" },
            { Problem.Crashes, "crashes constantly" },
            { Problem.DoesNotFit, "doesn't fit" },
            { Problem.WrongStyle, "isn't my style" }
        };
    }

    public enum Problem
    {
        DoesntRun,
        DoesntTurnOn,
        Jams,
        IsDefective,
        TerribleExperience,
        IsBuggy,
        HasSpyware,
        RunsSlowly,
        Crashes,
        DoesNotFit,
        WrongStyle,
    }
}
