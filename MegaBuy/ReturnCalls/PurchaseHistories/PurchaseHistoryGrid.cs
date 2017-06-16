using System;
using System.Collections.Generic;
using MegaBuy.Calls.Events;
using MegaBuy.PurchaseHistories;
using MegaBuy.ReturnCalls.Callers;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    public class PurchaseHistoryGrid : IVisualControl
    {
        private readonly GridLayout _grid;

        public ClickUIBranch Branch { get; }

        public PurchaseHistoryGrid(Size2 size)
        {
            Branch = new ClickUIBranch("Purchase History", (int)ClickUIPriorities.Pad);
            _grid = new GridLayout(size, 1, new List<Definition> { new ShareDefintion() });

            var purchases = new NewPurchaseHistoriesUI(_grid.GetBlockSize(1, 1, 2, 1));
            var purchase = new NewPurchaseUI(_grid.GetBlockSize(1, 1, 2, 1));

            _grid.AddSpatial(purchases, new Transform2(_grid.GetBlockSize(1, 1)), 1, 1);
            _grid.AddSpatial(purchase, purchase.Transform, 1, 1);

            Branch.Add(purchases.Branch);
            Branch.Add(purchase.Branch);
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _grid.Draw(parentTransform);
        }

        private void OnCallResolved()
        {
            World.Publish(new PurchasesListed());
        }
    }
}
