using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using MegaBuy.JobRoles.Referrer.Options;
using MonoDragons.Core.Common;

namespace MegaBuy.JobRoles.Referrer
{
    public class ReferrerLevel1Calls
    {
        private static readonly List<ICallOption> RoleOptions = new List<ICallOption>
        {
            new ReferToInfo(),
            new ReferToReturns(),
            new ReferToTroubleshooting(),
            new EscalateCall(),
        };

        private static string Player => GameState.CharName;

        private static readonly Func<Call>[] _calls = {
            () => Create((c, s) => c.CallerSays("I want to return this dumb " + s.Product + "!"), CallResolution.ReferToReturns),
            () => Create((c, s) => c.CallerSays(s.Product + " needs to be returned. It " + Problems.description[s.Problem] + "."), CallResolution.ReferToReturns),
            () => Create((c, s) => c.CallerSays("I need help. My " + s.Product + " " + Problems.description[s.Problem] + "."), CallResolution.ReferToTroubleshooting),
            () => Create(c => c.CallerSays("How much can I sell my " + Products.Random + " for?"), CallResolution.ReferToInfo),
            () => Create(c => c.CallerSays("MY " + Products.Random.ToUpper() + " DOESN'T WORK AND I NEED HELP RIGHT NOW!!!"), CallResolution.ReferToTroubleshooting),
            () => Create(c => c.CallerSays("Can I speak with accounting?"), CallResolution.EscalateCall),
            () => Create(c => c.CallerSays("I want to file an official complaint!"), CallResolution.EscalateCall),
            () => {
                return Create(c => {
                    c.CallerSays("Sup nigga.");
                    c.PlayerSays("Excuse me?");
                    c.CallerSays("I need help with my " + Products.Random + ", bitch.");
                }, CallResolution.ReferToTroubleshooting);
            },
        };

        private static Call Create(Action<Script> scriptBuilder, CallResolution correctOption)
        {
            var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel1);
            var script = InitScript();
            scriptBuilder(script);
            return new Call(scenario.Caller, script, correctOption, RoleOptions);
        }

        private static Call Create(Action<Script, CallScenario> scriptBuilder, CallResolution correctOption)
        {
            var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel1);
            var script = InitScript();
            scriptBuilder(script, scenario);
            return new Call(scenario.Caller, script, correctOption, RoleOptions);
        }

        private static readonly List<string> _introductions = new List<string>()
        {
            "You've reached MegaBuy, how may I direct your call?",
            "Hello there, how can I help you today?",
            "Hi there, how can I help you today?",
            "MegaBuy. What do you need?",
        };

        private static Script InitScript()
        {
            return new Script {{CallRole.Player, _introductions.Random()}};
        }

        public static Call NewCall()
        {
            return _calls.Random().Invoke();
        }
    }
}
