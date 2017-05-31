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
    public class PurchaseHistoryApp : IApp
    {
        private PurchasesUI _purchases;
        private PurchaseDetailUI _detail;

        public App Type => App.PurchaseHistory;
        public ClickUIBranch Branch { get; }

        public PurchaseHistoryApp()
        {
            Branch = new ClickUIBranch("Purchase HistoryApp", (int)ClickUIPriorities.Pad);
            _purchases = new PurchasesUI(Branch);
            _detail = new PurchaseDetailUI(Branch);
            World.Subscribe(EventSubscription.Create<CallResolved>(x => World.Publish(new AppChanged(App.Call)), this));
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            _purchases.Draw(parentTransform);
            _detail.Draw(parentTransform);
        }
    }
}
