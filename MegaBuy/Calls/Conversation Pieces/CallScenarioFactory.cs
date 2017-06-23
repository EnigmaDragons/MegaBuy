using MegaBuy.Calls.Callers;
using System.Collections.Generic;
using MegaBuy.Calls.Messages;
using MegaBuy.Jobs;
using MegaBuy.PurchaseHistories.Data;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public static class CallScenarioFactory
    {
        public static CallScenario Create(Job job, int patienceLossRateMs)
        {
            return Create(job, patienceLossRateMs, new Dictionary<string, string>());
        }

        public static CallScenario Create(Job job, int patienceLossRateMs, Dictionary<string, string> traits)
        {
            var product = Products.Random;
            var chat = new Chat();
            return new CallScenario
            {
                Chat = chat,
                Caller = new Caller(chat, patienceLossRateMs, traits),
                Product = product,
                Problem = Products.GetProblemFor(product.Name)
            };
        }
    }
}
