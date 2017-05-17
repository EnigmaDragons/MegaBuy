using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Common;

namespace MegaBuy.Jobs.ReturnSpecialist
{
    public static class ReturnSpecialistCalls
    {
        private static readonly List<ICallOption> Level1Options = new List<ICallOption>
        {
            new CallResolutionOption(CallResolution.ApproveReturn, "Approve Return"),
            new CallResolutionOption(CallResolution.ApproveReplacement, "Approve Replacement"),
            new CallResolutionOption(CallResolution.Reject, "Reject Request"),
        };

        private static readonly List<Func<Call>> Level1Calls = new List<Func<Call>>
        {
            () => CreateLvl1((c, s) => c.CallerSays($"I want to return this dumb {s.Product}!"), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"Can I get a replacement for {s.Product}? Mine is dead."), CallResolution.ApproveReplacement),
            () => CreateLvl1((c, s) => c.CallerSays($"Hello, I would like to return this {s.Product}."), CallResolution.Reject),
            () => CreateLvl1((c, s) => c.CallerSays($"This {s.Product} {Problems.Description[s.Problem]}, let me return it."), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"My {s.Product} {Problems.Description[s.Problem]}. I need a replacement."), CallResolution.ApproveReplacement),
            () => CreateLvl1((c, s) => c.CallerSays($"I'm going to ship your this pair of broken sandals."), CallResolution.Reject),
            () => CreateLvl1((c, s) => c.CallerSays($"This is third time you bastards have shipped me a defective {s.Product}. Take it back!"), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"You sent me the wrong item. I ordered {s.Product}."), CallResolution.ApproveReplacement),
        };

        private static Call CreateLvl1(Action<Script, CallScenario> scriptBuilder, CallResolution correctOption)
        {
            var scenario = CallScenarioFactory.Create(JobRole.ReturnSpecialistLevel1, PatienceLevel.Random);
            var script = InitScript();
            scriptBuilder(script, scenario);
            return new Call(scenario.Caller, script, correctOption, Level1Options);
        }

        private static readonly List<string> Introductions = new List<string>()
        {
            "Hello there, how can I help you today?",
            "Hi there, how can I help you today?",
            "Hello, I understand you are having problems with a product.",
            "Good day.",
        };

        private static Script InitScript()
        {
            return new Script { { CallRole.Player, Introductions.Random() } };
        }
    }
}
