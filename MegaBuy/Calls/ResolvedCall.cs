using MegaBuy.Calls.Rules;
using MegaBuy.PurchaseHistories;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls
{
    public sealed class ResolvedCall
    {
        public Optional<Purchase> Purchase { get; }
        public CallResolution CorrectResolution { get; }
        public CallResolution SelectedResolution { get; }

        public ResolvedCall(Optional<Purchase> purchase, CallResolution correct, CallResolution selected)
        {
            Purchase = purchase;
            CorrectResolution = correct;
            SelectedResolution = selected;
        }
    }
}
