using System;
using System.Collections.Generic;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.PositionBasedCall
{
    public static class ReferrerLevel2
    {
        private static readonly List<ICallOption> RoleOptions = new List<ICallOption>
        {
            new ReferToTroubleshooting(),
            new ReferToInfo(),
            new ReferToReturns(),
            new ReferToCareers(),
        };

        private static readonly Func<Call>[] _calls = {
            () => {
                var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel2);
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(scenario.Player, $"{scenario.Player} here. How may I direct your call?."));
                script.Add(new ScriptLine(name, $"I bought {scenario.Product} from you guys."));
                script.Add(new ScriptLine(name, "It really isn't meeting my expectations. I need to return it."));
                return new Call(scenario.Caller, script, CallResolution.ReferToReturns, RoleOptions);
            },
            () => {
                var scenario = CallScenarioFactory.Create(JobRole.ReferrerLevel2);
                var name = CallerNames.Random();
                var script = new Script();
                script.Add(new ScriptLine(scenario.Player, $"{scenario.Player} here. How may I direct your call?."));
                script.Add(new ScriptLine(name, "I saw a posting for a new Senior Vice Product Executive position."));
                script.Add(new ScriptLine(name, "I wish to apply."));
                return new Call(scenario.Caller, script, CallResolution.ReferToCareers, RoleOptions);
            },
        };

        public static Call NewCall()
        {
            return _calls.Random().Invoke();
        }
    }
}
