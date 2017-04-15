
namespace MegaBuy.Calls.Events
{
    public class CallStarted
    {
        public Call Call { get; }

        public CallStarted(Call call)
        {
            Call = call;
        }
    }
}
