using MegaBuy.Calls.Callers;
using System.Collections.Generic;
using MegaBuy.Jobs;
using MegaBuy.PurchaseHistories.Data;

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
                Product = product.Name,
                Problem = Products.GetProblemFor(product.Name)
            };
        }
    }
}
