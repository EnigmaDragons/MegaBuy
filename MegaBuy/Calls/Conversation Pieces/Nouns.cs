using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class Nouns
    {
        public static Dictionary<VerbAttribute, string[]> nouns = new Dictionary<string, VerbAttribute[]> {
            { "Flying Car", new VerbAttribute[] { VerbAttribute.CanRun, VerbAttribute.CanTurnOnOrOff } }
        };
    }
}
