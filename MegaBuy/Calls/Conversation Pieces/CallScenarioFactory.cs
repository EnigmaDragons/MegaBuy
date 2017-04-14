using MegaBuy.Calls.Rules;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public static class CallScenarioFactory
    {
        public static CallScenario Create(JobRole jobRole)
        {
            var product = Products.Random;
            return new CallScenario
            {
                Player = GameState.CharName,
                Caller = new Caller(),
                Product = product,
                Problem = Products.GetProblemFor(product)
            };
        }
    }
}
