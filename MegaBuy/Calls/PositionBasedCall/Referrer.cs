using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls.PositionBasedCall
{
    public class Referrer
    {
        private static Func<string, Call>[] calls = new Func<string, Call>[]
        {
            (player) => {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello my name is " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                var verb = Verbs.verbs.Random();
                var noun = Nouns.nouns[verb.Value].Random();
                script.Add(new ScriptLine(name, "My " + noun + " " + Verbs.canNotSynonyms.Random() + " " + verb.Key + "."));
                return new Call(new Caller(2000), script, CallResolution.ReferToTroubleshooting, new List<ICallOption>
                    { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) => {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello my name is " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Nouns.nouns.Random().Value.Random() + " for?"));
                return new Call(new Caller(2000), script, CallResolution.ReferToInfo, new List<ICallOption>
                    { new ReferToTroubleshooting(), new ReferToInfo() });
            }
        };

        public static Call NewCall(string player)
        {
            return calls.Random()(player);
        }
    }
}
