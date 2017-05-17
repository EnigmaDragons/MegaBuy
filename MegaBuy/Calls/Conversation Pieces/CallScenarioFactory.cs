using MegaBuy.Calls.Callers;
using System.Collections.Generic;
using MegaBuy.Jobs;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public static class CallScenarioFactory
    {
        public static CallScenario Create(JobRole jobRole, int patienceLossRateMs)
        {
            return Create(jobRole, patienceLossRateMs, new Dictionary<string, string>());
        }

        public static CallScenario Create(JobRole jobRole, int patienceLossRateMs, Dictionary<string, string> traits)
        {
            var product = Products.Random;
            return new CallScenario
            {
                Caller = new Caller(patienceLossRateMs, traits),
                Product = product,
                Problem = Products.GetProblemFor(product)
            };
        }
    }
}
