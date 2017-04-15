using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using System;
using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Messages;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.PositionBasedCall
{
    public class ReferrerLevel1
    {
        private static readonly List<ICallOption> RoleOptions = new List<ICallOption> { new ReferToTroubleshooting(), new ReferToInfo() };

        private static string Player => GameState.CharName;

        private static readonly Func<Call>[] _calls = {
            () =>
            {
                var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel1);
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(scenario.Player, "Hello my name is " + scenario.Player + "."));
                script.Add(new ScriptLine(name, "Hello " + scenario.Player + ", I need help."));
                script.Add(new ScriptLine(name, "My " + scenario.Product + " " + Problems.description[scenario.Problem] + "."));
                return new Call(scenario.Caller, script, CallResolution.ReferToTroubleshooting, RoleOptions);
            },
            () => {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hello, my name is " + name + "."));
                script.Add(new ScriptLine(Player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Products.Random + " for?"));
                return new Call(new Caller(PatienceLevel.Impatient), script, CallResolution.ReferToInfo, RoleOptions);
            },
            () => {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "I need to sell my " + Products.Random + " right now."));
                script.Add(new ScriptLine(Player, "Hi " + name + ", before I help, what is your name?"));
                script.Add(new ScriptLine(name, name + "."));
                return new Call(new Caller(PatienceLevel.Impatient), script, CallResolution.ReferToInfo, RoleOptions);
            },
            () =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "Hey i'm " + name + "."));
                script.Add(new ScriptLine(Player, "Hello " + name + ", how can I help you?"));
                script.Add(new ScriptLine(name, "How much can I sell my " + Products.Random + " for?"));
                return new Call(new Caller(PatienceLevel.VeryPatient), script, CallResolution.ReferToInfo, RoleOptions);
            },
            () =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(name, "MY " + Products.Random.ToUpper() + " DOESN'T WORK AND I NEED HELP RIGHT NOW!!!"));
                script.Add(new ScriptLine(Player, "Please calm down, can you tell me your name please?"));
                script.Add(new ScriptLine(name, name.ToUpper() + " . ITS NOT WORKING!!! HELP!!!"));
                return new Call(new Caller(PatienceLevel.VeryImpatient), script, CallResolution.ReferToTroubleshooting, RoleOptions);
            },
            () =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(Player, "Hi there, how can I help you today?"));
                script.Add(new ScriptLine(name, "Sup nigga."));
                script.Add(new ScriptLine(Player, "Excuse me?"));
                script.Add(new ScriptLine(name, "I need help with my " + Products.Random + ", bitch."));
                return new Call(new Caller(PatienceLevel.Patient), script, CallResolution.ReferToTroubleshooting, RoleOptions);
            },
            () =>
            {
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(Player, "Hello there, how can I help you today?"));
                script.Add(new ScriptLine(name, "YOU FUCKING PLEKE!!! GET OFF YOUR ASS AND HELP ME!!!"));
                script.Add(new ScriptLine(Player, "Calm down, what do you need help with?"));
                script.Add(new ScriptLine(name, "HELP ME FIX MY " + Products.Random.ToUpper() + " NOW."));
                return new Call(new Caller(PatienceLevel.Impatient), script, CallResolution.ReferToTroubleshooting, RoleOptions);
            },
        };

        public static Call NewCall()
        {
            return _calls.Random().Invoke();
        }
    }
}
