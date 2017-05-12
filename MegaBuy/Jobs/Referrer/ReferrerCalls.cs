using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Common;

namespace MegaBuy.Jobs.Referrer
{
    public class ReferrerCalls
    {
        private static readonly List<ICallOption> Level1Options = new List<ICallOption>
        {
            new CallResolutionOption(CallResolution.ReferToInfo, "Info"),
            new CallResolutionOption(CallResolution.ReferToReturns, "Returns"),
            new CallResolutionOption(CallResolution.ReferToTroubleshooting, "Troubleshooting"),
            new CallResolutionOption(CallResolution.EscalateCall, "Escalate"),
        };
        
        private static readonly List<Func<Call>> Level1Calls = new List<Func<Call>> {
            () => CreateLvl1((c, s) => c.CallerSays("I want to return this dumb " + s.Product + "!"), CallResolution.ReferToReturns, Traits.None),
            () => CreateLvl1((c, s) => c.CallerSays(s.Product + " needs to be returned. It " + Problems.description[s.Problem] + "."), CallResolution.ReferToReturns, Traits.None),
            () => CreateLvl1((c, s) => c.CallerSays("I need help. My " + s.Product + " " + Problems.description[s.Problem] + "."), CallResolution.ReferToTroubleshooting, Traits.None),
            () => CreateLvl1((c, s) => c.CallerSays("How much can I sell my " + Products.Random + " for?"), CallResolution.ReferToInfo, Traits.None),
            () => CreateLvl1((c, s) => c.CallerSays("MY " + Products.Random.ToUpper() + " DOESN'T WORK AND I NEED HELP RIGHT NOW!!!"), CallResolution.ReferToTroubleshooting, Traits.None),
            () => CreateLvl1((c, s) => c.CallerSays("Can I speak with accounting?"), CallResolution.EscalateCall, Traits.None),
            () => CreateLvl1((c, s) => c.CallerSays("I want to file an official complaint!"), CallResolution.EscalateCall, Traits.None),
            () => {
                return CreateLvl1((c, s) => {
                    c.CallerSays("Sup nigga.");
                    c.PlayerSays("Excuse me?");
                    c.CallerSays("I need help with my " + Products.Random + ", bitch.");
                }, CallResolution.ReferToTroubleshooting, Traits.None);
            },
        };

        private static readonly List<ICallOption> Level2Options = new List<ICallOption>
        {
            new CallResolutionOption(CallResolution.ReferToOrders, "Orders"),
            new CallResolutionOption(CallResolution.ReferToTroubleshooting, "Troubleshooting"),
            new CallResolutionOption(CallResolution.ReferToInfo, "Info"),
            new CallResolutionOption(CallResolution.ReferToReturns, "Returns"),
            new CallResolutionOption(CallResolution.ReferToCareers, "Careers"),
            new CallResolutionOption(CallResolution.EscalateCall, "Escalate"),
        };

        private static readonly List<Func<Call>> Level2Calls = Level1Calls.Union(new List<Func<Call>>
        {
            () => CreateLvl2((c, s) => c.CallerSays("I wish to apply for the new Senior Vice Product Executive position."), CallResolution.ReferToCareers, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays("Do you have any job openings?"), CallResolution.ReferToCareers, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays($"My {s.Product} hasn't arrived yet."), CallResolution.ReferToOrders, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays("I think I accidentally ordered something by mistake."), CallResolution.ReferToOrders, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays($"How many copies of {s.Product} did I order?"), CallResolution.ReferToOrders, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays($"I want to buy {s.Product}."), CallResolution.EscalateCall, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays($"When will you get more {s.Product} in stock?"), CallResolution.ReferToInfo, Traits.None),
            () => CreateLvl2((c, s) => c.CallerSays($"I just moved. I need to change my delivery address"), CallResolution.ReferToOrders, Traits.None),
        }).ToList();
        
        private static readonly List<ICallOption> Level3Options = new List<ICallOption>
        {
            new CallResolutionOption(CallResolution.ReferToOrders, "Orders"),
            new CallResolutionOption(CallResolution.ReferToTroubleshooting, "Troubleshooting"),
            new CallResolutionOption(CallResolution.ReferToInfo, "Info"),
            new CallResolutionOption(CallResolution.ReferToReturns, "Returns"),
            new CallResolutionOption(CallResolution.ReferToCareers, "Careers"),
            new CallResolutionOption(CallResolution.ReferToFeedback, "Feedback"),
            new CallResolutionOption(CallResolution.ReferToAccounting, "Accounting"),
            new CallResolutionOption(CallResolution.ReferToRecommendations, "Recommendations"),
            new CallResolutionOption(CallResolution.ReferToLegal, "Legal"),
            new CallResolutionOption(CallResolution.ReferToGeneralist, "Generalist"),
        };

        private static readonly List<Func<Call>> Level3Calls = new List<Func<Call>>
        {
            () => CreateLvl3((c, s) => c.CallerSays("Do you have any new music that I would be interested in?"), CallResolution.ReferToRecommendations, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("I need help picking out a toy for my little boy"), CallResolution.ReferToRecommendations, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("My company relocated our headquarters, I need to update our payment address"), CallResolution.ReferToAccounting, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("I have a question about an invoice discrepancy"), CallResolution.ReferToAccounting, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("I would like to lodge a complaint about a customer support person"), CallResolution.ReferToFeedback, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("Your new Essentials marketing campaign is corrupting children."), CallResolution.ReferToFeedback, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("Can you help me schedule a corporate party?"), CallResolution.ReferToGeneralist, Traits.None),
            () => CreateLvl3((c, s) => c.CallerSays("Let me speak with your lawyers"), CallResolution.ReferToLegal, Traits.None),
            // @ todo #1 Create 8 more Referrer Level 3 call scripts

        };

        private static Call CreateLvl1(Action<Script, CallScenario> scriptBuilder, CallResolution correctOption, Dictionary<string, string> map)
        {
            var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel1, PatienceLevel.Random, map);
            var script = InitScript();
            scriptBuilder(script, scenario);
            return new Call(scenario.Caller, script, correctOption, Level1Options);
        }

        private static Call CreateLvl2(Action<Script, CallScenario> scriptBuilder, CallResolution correctOption, Dictionary<string, string> map)
        {
            var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel2, PatienceLevel.Random, map);
            var script = InitScript();
            scriptBuilder(script, scenario);
            return new Call(scenario.Caller, script, correctOption, Level2Options);
        }

        private static Call CreateLvl3(Action<Script, CallScenario> scriptBuilder, CallResolution correctOption, Dictionary<string, string> map)
        {
            var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel3, PatienceLevel.Random, map);
            var script = InitScript();
            scriptBuilder(script, scenario);
            return new Call(scenario.Caller, script, correctOption, Level3Options);
        }

        private static readonly List<string> Introductions = new List<string>()
        {
            "You've reached MegaBuy, how may I direct your call?",
            "Hello there, how can I help you today?",
            "Hi there, how can I help you today?",
            "MegaBuy. What do you need?",
        };

        private static Script InitScript()
        {
            return new Script {{CallRole.Player, Introductions.Random()}};
        }

        public static Call NewLevel1Call()
        {
            return Level1Calls.Random().Invoke();
        }

        public static Call NewLevel2Call()
        {
            return Level2Calls.Random().Invoke();
        }

        public static Call NewLevel3Call()
        {
            return Level3Calls.Random().Invoke();
        }
    }
}
