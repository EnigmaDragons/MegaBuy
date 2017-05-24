using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseHistory : IApp
    {
        private PurchasesUI _purchases;
        private PurchaseDetailUI _detail;

        public App Type => App.PurchaseHistory;
        public ClickUIBranch Branch { get; }

        public PurchaseHistory()
        {
            Branch = new ClickUIBranch("Purchase History", (int)ClickUIPriorities.Pad);
            _purchases = new PurchasesUI(Branch);
            _detail = new PurchaseDetailUI(Branch);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => PullPurchases(x.Call), this));
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            _purchases.Draw(parentTransform);
            _detail.Draw(parentTransform);
        }

        public void PullPurchases(Call call)
        {
            _purchases.Dispose();
            _detail.Dispose();
            _purchases = new PurchasesUI(Branch);
            _detail = new PurchaseDetailUI(Branch);
        }
    }
}
