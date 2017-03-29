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
        private static Func<Call>[] calls = new Func<Call>[]
        {
            () => {
                var name = CallerNames.GetRandomName();
                var player = "Player";
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello my name is " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                var verb = Verbs.GetRandomCantVerb();
                var noun = Nouns.GetRandomNounWithAttribute(verb.Value);
                script.Add(new ScriptLine(name, "My " + noun + " " + verb + "."));
                return new Call(new Caller(2000), script, CallResolution.ReferToTroubleshooting, new List<ICallOption>
                    { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            () => {
                var name = CallerNames.names[Rng.Int(CallerNames.names.Length)];
                var player = "Player";
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello my name is " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Nouns.GetRandomNoun()));
                return new Call(new Caller(2000), script, CallResolution.ReferToInfo, new List<ICallOption>
                    { new ReferToTroubleshooting(), new ReferToInfo() });
            }
        };

        public static Call NewCall()
        {
            return calls[Rng.Int(calls.Length)]();
        }
    }
}
