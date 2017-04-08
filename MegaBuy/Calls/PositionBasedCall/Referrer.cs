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
                script.Add(new ScriptLine(player, "Hello my name is " + player + "."));
                script.Add(new ScriptLine(name, "Hello " + player + ", I need help."));
                var verb = Verbs.verbs.Random();
                var noun = Nouns.nouns[verb.Value].Random();
                script.Add(new ScriptLine(name, "My " + noun + " " + Verbs.canNotSynonyms.Random() + " " + verb.Key + "."));
                return new Call(new Caller(2000), script, CallResolution.ReferToTroubleshooting, new List<ICallOption>
                    { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) => {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello, my name is " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Nouns.nouns.Random().Value.Random() + " for?"));
                return new Call(new Caller(2500), script, CallResolution.ReferToInfo, new List<ICallOption>
                    { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) => {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "I need to sell my " + Nouns.nouns.Random().Value.Random() + " right now."));
                script.Add(new ScriptLine(player, "Hi " + name + ", before I help, what is your name?"));
                script.Add(new ScriptLine(name, name + "."));
                return new Call(new Caller(1500), script, CallResolution.ReferToInfo, new List<ICallOption>
                { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) =>
            {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hey i'm " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Nouns.nouns.Random().Value.Random() + " for?"));
                return new Call(new Caller(3000), script, CallResolution.ReferToInfo, new List<ICallOption>
                { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) =>
            {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "MY " + Nouns.nouns.Random().Value.Random().ToUpper() + " DOESN'T WORK AND I NEED HELP RIGHT NOW!!!"));
                script.Add(new ScriptLine(player, "Please calm down, can you tell me your name please?"));
                script.Add(new ScriptLine(name, name.ToUpper() + " . ITS NOT WORKING!!! HELP!!!"));
                return new Call(new Caller(1000), script, CallResolution.ReferToTroubleshooting, new List<ICallOption>
                { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) =>
            {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(player, "Hi there, how can I help you today?"));
                script.Add(new ScriptLine(name, "Sup nigga."));
                script.Add(new ScriptLine(player, "Excuse me?"));
                script.Add(new ScriptLine(name, "I need help with my " + Nouns.nouns.Random().Value.Random() + ", bitch."));
                return new Call(new Caller(2500), script, CallResolution.ReferToTroubleshooting, new List<ICallOption>
                { new ReferToTroubleshooting(), new ReferToInfo() });
            },
            (player) =>
            {
                var name = CallerNames.names.Random();
                var script = new Script();
                script.Add(new ScriptLine(player, "Hello there, how can I help you today?"));
                script.Add(new ScriptLine(name, "YOU FUCKING PLEKE!!! GET OFF YOUR ASS AND HELP ME!!!"));
                script.Add(new ScriptLine(player, "Calm down, what do you need help with?"));
                script.Add(new ScriptLine(name, "HELP ME FIX MY " + Nouns.nouns.Random().Value.Random().ToUpper() + " NOW."));
                return new Call(new Caller(1500), script, CallResolution.ReferToTroubleshooting, new List<ICallOption>
                { new ReferToTroubleshooting(), new ReferToInfo() });
            },
        };

        public static Call NewCall(string player)
        {
            return calls.Random()(player);
        }
    }
}
