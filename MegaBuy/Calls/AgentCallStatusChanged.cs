
namespace MegaBuy.Calls
{
    public sealed class AgentCallStatusChanged
    {
        public AgentCallStatus Status { get; }

        public AgentCallStatusChanged(AgentCallStatus status)
        {
            Status = status;
        }
    }
}
