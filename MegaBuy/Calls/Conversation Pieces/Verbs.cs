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
        public static Dictionary<string, VerbAttribute> cantVerbs = new Dictionary<string, VerbAttribute>
        {
            { "can't run", VerbAttribute.CanRun},
            { "can't turn on", VerbAttribute.CanTurnOnOrOff },
            { "can't turn off", VerbAttribute.CanTurnOnOrOff }
        };

        public static KeyValuePair<string, VerbAttribute> GetRandomCantVerb()
        {
            return cantVerbs.ElementAt(Rng.Int(cantVerbs.Count()));
        }
    }

    public enum VerbAttribute
    {
        CanRun,
        CanTurnOnOrOff
    }
}
