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
        private readonly List<ClickUIBranch> _branches = new List<ClickUIBranch>();
        private readonly GridLayout _grid;

        private bool _isViewingPurchases = false;

        public ClickUIBranch Branch { get; }

        public PurchaseHistoryGrid(Size2 size)
        {
            Branch = new ClickUIBranch("Purchase History", (int)ClickUIPriorities.Pad);
            _grid = new GridLayout(size, 2, new List<Definition> { new ShareDefintion(), new ConcreteDefinition(70) });

            var purchases = new NewPurchaseHistoriesUI(_grid.GetBlockSize(1, 1, 2, 1));
            var purchase = new NewPurchaseUI(_grid.GetBlockSize(1, 1, 2, 1));
            var callerButton = new ViewCallerUI();
            var listPurchasesButton = new ViewPurchaseListUI();

            _grid.AddSpatial(purchases, new Transform2(_grid.GetBlockSize(1, 1, 2, 1)), 1, 1, 2, 1);
            _grid.AddSpatial(purchase, new Transform2(_grid.GetBlockSize(1, 1, 2, 1)), 1, 1, 2, 1);
            _grid.AddSpatial(callerButton, callerButton.Transform, 2, 2);
            _grid.AddSpatial(listPurchasesButton, listPurchasesButton.Transform, 1, 2);

            _branches.Add(purchases.Branch);
            _branches.Add(callerButton.Branch);
            _branches.Add(listPurchasesButton.Branch);
            World.Subscribe(EventSubscription.Create<PurchaseHistoryViewed>(x => OnPurchaseHistoryViewed(), this));
            World.Subscribe(EventSubscription.Create<CallerViewed>(x => OnCallerViewed(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isViewingPurchases)
                _grid.Draw(parentTransform);
        }

        private void OnPurchaseHistoryViewed()
        {
            _isViewingPurchases = true;
            _branches.ForEach(x => Branch.Add(x));
        }

        private void OnCallerViewed()
        {
            _isViewingPurchases = false;
            _branches.ForEach(x => Branch.Remove(x));
        }

        private void OnCallResolved()
        {
            World.Publish(new CallerViewed());
            World.Publish(new PurchasesListed());
        }
    }
}
