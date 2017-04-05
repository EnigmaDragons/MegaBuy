using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class Verbs
    {
        public static Dictionary<string, NounOperations> verbs = new Dictionary<string, NounOperations>
        {
            { "run", NounOperations.CanRun},
            { "turn on", NounOperations.CanTurnOnOrOff },
            { "turn off", NounOperations.CanTurnOnOrOff }
        };

        public static string[] canNotSynonyms = new string[] { "can't", "fails to", "is unable to" };
    }

    public enum NounOperations
    {
        CanRun,
        CanTurnOnOrOff
    }
}
