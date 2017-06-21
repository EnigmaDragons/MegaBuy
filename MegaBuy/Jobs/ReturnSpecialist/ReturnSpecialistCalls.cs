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
        
        private static readonly List<Func<Call>> Calls = new List<Func<Call>>
        {
            () => Create((c, s) => c.CallerSays($"I want to return this dumb {s.ProductName}!"), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"Can I get a replacement for {s.ProductName}? Mine is dead."), CallResolution.ApproveReplacement),
            () => Create((c, s) => c.CallerSays($"Hello, I would like to return this {s.ProductName}."), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"This {s.ProductName} {Problems.Description[s.Problem]}, let me return it."), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"My {s.ProductName} {Problems.Description[s.Problem]}. I need a replacement."), CallResolution.ApproveReplacement),
            () => Create((c, s) => c.CallerSays($"I'm going to ship you this pair of broken sandals."), CallResolution.Reject),
            () => Create((c, s) => c.CallerSays($"This is third time you bastards have shipped me a defective {s.ProductName}. Take it back!"), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"You sent me the wrong item. I ordered {s.ProductName}."), CallResolution.ApproveReplacement),
            () => Create((c, s) => c.CallerSays($"I accidently ordered {s.ProductName}. Can you let me return it?"), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"My daughter didn't like her birthday gift: {s.ProductName}. I'm returning it."), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"I bought {s.ProductName}, but I don't need it anymore."), CallResolution.ApproveReturn),
            () => Create((c, s) => c.CallerSays($"This {s.ProductName} just isn't what I expected. I don't want it."), CallResolution.ApproveReturn),
            // @todo #1 Content: Write another 4 more scripts
        };
        
        private static Call Create(Action<Chat, CallScenario> scriptBuilder, CallResolution requestedOption)
        {
            var correctResolution = Rng.Between(requestedOption, CallResolution.Reject, 0.70);
            var scenario = CallScenarioFactory.Create(Job.ReturnSpecialistLevel1, PatienceLevel.Random);
            var chat = InitChat(scenario);
            scriptBuilder(chat, scenario);
            AddPlayerRequestConfirmation(chat);

            var purchase = CreatePurchase(scenario, chat, correctResolution);

            Debug.WriteLine($"CallResolution: Requested {requestedOption}. Expects {correctResolution} for {purchase.ProductName}");
            var history = Purchase.CreateInfiniteWith(purchase).Take(1000).Where(x => x.PurchasedWithinLast(90));
            scenario.Purchases = history;
            scenario.Target = new Optional<Purchase>(purchase);
            return new Call(chat, scenario, correctResolution, Level1Options);
        }

        private static void AddPlayerRequestConfirmation(Chat chat)
        {
            chat.PlayerSays("Let me see what I can do for you.");
        }

        private static Purchase CreatePurchase(CallScenario scenario, Chat chat, CallResolution correctResolution)
        {
            var policies = CurrentGameState.State.ActivePolicies;

            Purchase purchase;
            int numAttempts = 0;
            while (true)
            {
                numAttempts++;
                purchase = Purchase.Create(DateWithinDays(90), scenario.Product);
                scenario.Target = new Optional<Purchase>(purchase);
                // @todo #1 Backend: Change this to use a lighter-weight object that doesn't involve event subscriptions
                var call = new Call(chat, scenario, correctResolution, Level1Options);
                var violations = policies.GetViolations(correctResolution, call);
                call.Dispose();
                if (correctResolution == CallResolution.Reject && violations.Any())
                    break;
                if (!violations.Any())
                    break;
            }

            Debug.WriteLine($"Created target purchase in {numAttempts} attempts");
            return purchase;
        }

        private static DateTime DateWithinDays(int days)
        {
            return CurrentGameState.State.DateTime.AddDays(-Rng.Int(days));
        }

        private static readonly List<Func<CallScenario, string>> Introductions = new List<Func<CallScenario, string>>
        {
            s => "Hello there, how can I help you today?",
            s => "Hi, how can I help you?",
            s => "Hello, I understand you are having problems with a product.",
            s => "Good day, thank you for calling MegaBuy. How may I help you?",
            s => "We appreciate your business. What can I do for you?",
            s => "Thank you for calling MegaBuy! What would you like today?",
            s => "Do you need help with a return or replacement?",
            s => $"Hello {s.Caller.FirstName}. How can I assist you?",
        };

        private static Chat InitChat(CallScenario scenario)
        {
            return scenario.Chat.PlayerSays(Introductions.Random().Invoke(scenario));
        }

        public static Call NewCall()
        {
            return Calls.Random().Invoke();
        }
    }
}
