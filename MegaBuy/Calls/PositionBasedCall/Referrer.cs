using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using System;
using System.Collections.Generic;

namespace MegaBuy.Calls.PositionBasedCall
{
    public class Referrer
    {
        private static List<ICallOption> DayOneOptions = new List<ICallOption> { new ReferToTroubleshooting(), new ReferToInfo() };

        private static readonly Func<string, Call>[] _calls = {
            (player) => {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(player, "Hello my name is " + player + "."));
                script.Add(new ScriptLine(name, "Hello " + player + ", I need help."));
                var product = Products.Random();
                var operation = Products.GetOperations(product).Random();
                script.Add(new ScriptLine(name, "My " + product + " " + Problems.description[operation] + "."));
                return new Call(new Caller(PatienceLevel.Average), script, CallResolution.ReferToTroubleshooting, DayOneOptions);
            },
            (player) => {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello, my name is " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Products.Random() + " for?"));
                return new Call(new Caller(PatienceLevel.Impatient), script, CallResolution.ReferToInfo, DayOneOptions);
            },
            (player) => {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "I need to sell my " + Products.Random() + " right now."));
                script.Add(new ScriptLine(player, "Hi " + name + ", before I help, what is your name?"));
                script.Add(new ScriptLine(name, name + "."));
                return new Call(new Caller(PatienceLevel.Impatient), script, CallResolution.ReferToInfo, DayOneOptions);
            },
            (player) =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hey i'm " + name + "."));
                script.Add(new ScriptLine(player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Products.Random() + " for?"));
                return new Call(new Caller(PatienceLevel.VeryPatient), script, CallResolution.ReferToInfo, DayOneOptions);
            },
            (player) =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "MY " + Products.Random().ToUpper() + " DOESN'T WORK AND I NEED HELP RIGHT NOW!!!"));
                script.Add(new ScriptLine(player, "Please calm down, can you tell me your name please?"));
                script.Add(new ScriptLine(name, name.ToUpper() + " . ITS NOT WORKING!!! HELP!!!"));
                return new Call(new Caller(PatienceLevel.VeryImpatient), script, CallResolution.ReferToTroubleshooting, DayOneOptions);
            },
            (player) =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(player, "Hi there, how can I help you today?"));
                script.Add(new ScriptLine(name, "Sup nigga."));
                script.Add(new ScriptLine(player, "Excuse me?"));
                script.Add(new ScriptLine(name, "I need help with my " + Products.Random() + ", bitch."));
                return new Call(new Caller(PatienceLevel.Patient), script, CallResolution.ReferToTroubleshooting, DayOneOptions);
            },
            (player) =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(player, "Hello there, how can I help you today?"));
                script.Add(new ScriptLine(name, "YOU FUCKING PLEKE!!! GET OFF YOUR ASS AND HELP ME!!!"));
                script.Add(new ScriptLine(player, "Calm down, what do you need help with?"));
                script.Add(new ScriptLine(name, "HELP ME FIX MY " + Products.Random().ToUpper() + " NOW."));
                return new Call(new Caller(PatienceLevel.Impatient), script, CallResolution.ReferToTroubleshooting, DayOneOptions);
            },
        };

        public static Call NewCall(string player)
        {
            return _calls.Random()(player);
        }
    }
}
