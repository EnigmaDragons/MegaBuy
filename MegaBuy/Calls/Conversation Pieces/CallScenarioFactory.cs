using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Jobs;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public static class CallScenarioFactory
    {
        public static CallScenario Create(JobRole jobRole, int patienceLossRateMs, Dictionary<string, string> traits)
        {
            var product = Products.Random;
            return new CallScenario
            {
                Player = GameState.CharName,
                Caller = new Caller(patienceLossRateMs, traits),
                Product = product,
                Problem = Products.GetProblemFor(product)
            };
        }
    }
}
