using MegaBuy.Money;
using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Options
{
    public sealed class ReferToInfo : ICallOption
    {
        public string message
        {
            get
            {
                return "Refer the caller to information.";
            }
        }

        public void Go(bool IsCorrect)
        {
            if (!IsCorrect)
            {
                new TechnicalMistakeOccurred(new Fee(2));
                World.Publish(new CallFailed());
            }
            World.Publish(new CallSucceeded());
        }
    }
}
