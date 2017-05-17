using MegaBuy.Calls.Callers;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class CallScenario
    {
        // @todo #1 don't assume there is exactly 1 product problem.
        public Caller Caller { get; set; }
        public string Product { get; set; }
        public Problem Problem { get; set; }
    }
}
