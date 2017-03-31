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
        public static Dictionary<VerbAttribute, string[]> nouns = new Dictionary<VerbAttribute, string[]>
        {
            { VerbAttribute.CanRun, new string[] { "flying car", "smart computer" } },
            { VerbAttribute.CanTurnOnOrOff, new string[] { "flying car", "smart computer" } }
        };

        public static string GetRandomNounWithAttribute(VerbAttribute attribute)
        {
            return nouns[attribute][Rng.Int(nouns[attribute].Count())];
        }

        public static string GetRandomNoun()
        {
            var attributesNouns = nouns.ElementAt(Rng.Int(nouns.Count())).Value;
            return attributesNouns[Rng.Int(attributesNouns.Length)];
        }
    }
}
