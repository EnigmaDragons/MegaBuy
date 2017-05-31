using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using MegaBuy.PurchaseHistories;
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
            () => CreateLvl1((c, s) => c.CallerSays($"I want to return this dumb {s.ProductName}!"), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"Can I get a replacement for {s.ProductName}? Mine is dead."), CallResolution.ApproveReplacement),
            () => CreateLvl1((c, s) => c.CallerSays($"Hello, I would like to return this {s.ProductName}."), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"This {s.ProductName} {Problems.Description[s.Problem]}, let me return it."), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"My {s.ProductName} {Problems.Description[s.Problem]}. I need a replacement."), CallResolution.ApproveReplacement),
            () => CreateLvl1((c, s) => c.CallerSays($"I'm going to ship you this pair of broken sandals."), CallResolution.Reject),
            () => CreateLvl1((c, s) => c.CallerSays($"This is third time you bastards have shipped me a defective {s.ProductName}. Take it back!"), CallResolution.ApproveReturn),
            () => CreateLvl1((c, s) => c.CallerSays($"You sent me the wrong item. I ordered {s.ProductName}."), CallResolution.ApproveReplacement),
        };

        // @todo #1: Content: Create ReturnSpecialistLevel2 Calls
        
        private static Call CreateLvl1(Action<Script, CallScenario> scriptBuilder, CallResolution requestedOption)
        {
            var correctResolution = Rng.Between(requestedOption, CallResolution.Reject, 0.70);
            var scenario = CallScenarioFactory.Create(Job.ReturnSpecialistLevel1, PatienceLevel.Random);
            var script = InitScript();
            scriptBuilder(script, scenario);
            // @todo #1: Backend: Build purchases based on policies, instead of human knowledge
            var purchase = Purchase.Create(DateWithinDays(30));
            if (correctResolution == CallResolution.ApproveReturn)
                purchase = Purchase.Create(DateWithinDays(30), scenario.Product, true, false, false);
            if (correctResolution == CallResolution.ApproveReplacement)
                purchase = Purchase.Create(DateWithinDays(60), scenario.Product, true, false, false);
            if (correctResolution == CallResolution.Reject)
                purchase = Purchase.Create(DateWithinDays(30), scenario.Product, true, true, false);
            Debug.WriteLine($"CallResolution: Requested {requestedOption}. Expects {correctResolution} for {purchase.ProductName}");
            var history = Purchase.CreateInfiniteWith(purchase).Take(300).ToList();
            scenario.Purchases = history;
            scenario.Target = new Optional<Purchase>(purchase);
            return new Call(script, scenario, correctResolution, Level1Options);
        }

        private static DateTime DateWithinDays(int days)
        {
            return CurrentGameState.State.DateTime.AddDays(-Rng.Int(days));
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

        public static Call NewLevel1Call()
        {
            return Level1Calls.Random().Invoke();
        }
    }
}
