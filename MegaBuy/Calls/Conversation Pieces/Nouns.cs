using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class Nouns
    {
        public static Dictionary<NounOperations, string[]> nouns = new Dictionary<NounOperations, string[]>
        {
            { NounOperations.CanRun, new string[] { "flying car", "smart computer" } },
            { NounOperations.CanTurnOnOrOff, new string[] { "flying car", "smart computer" } }
        };
    }
}
