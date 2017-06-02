using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Pads.Apps;
using MegaBuy.PurchaseHistories;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    class NewPurchaseHistoriesUI : IVisualControl
    {
        private readonly GridLayout _grid;
        private readonly List<ClickUIBranch> _branches = new List<ClickUIBranch>();

        private IEnumerator<Purchase> _purchaseSupplier;
        private readonly List<NewPurchaseSummaryUI> _purchaseUIs = new List<NewPurchaseSummaryUI>();
        private readonly int _ordersPerPage = 10;
        public ClickUIBranch Branch { get; }

        private int _index = 0;
        private bool _isListing = true;

        public NewPurchaseHistoriesUI(Size2 size)
        {
            Branch = new ClickUIBranch("Purchases", (int)ClickUIPriorities.Pad);
            _grid = new GridLayout(size, 
                new List<Definition> { new ConcreteDefinition(120), new ShareDefintion(), new ConcreteDefinition(120)}, 1);

            var backButton = ImageTextButtonFactory.CreateRotated("<<", Vector2.Zero, NavigateBack, () => _index != 0);
            var smartBackButton = new SmartControl(backButton, (int)ClickUIPriorities.Pad);
            Branch.Add(smartBackButton.Branch);
            _grid.AddSpatial(smartBackButton, backButton.Transform, 1, 1);

            var forwardButton = ImageTextButtonFactory.CreateRotated(">>", Vector2.Zero, NavigateForward, () => _purchaseSupplier.Current != null);
            var smartForwardButton = new SmartControl(forwardButton, (int)ClickUIPriorities.Pad);
            Branch.Add(smartForwardButton.Branch);
            _grid.AddSpatial(smartForwardButton, forwardButton.Transform, 3, 1);

            _branches.Add(smartForwardButton.Branch);
            _branches.Add(smartBackButton.Branch);

            World.Subscribe(EventSubscription.Create<PurchaseInspected>(x => Inspect(), this));
            World.Subscribe(EventSubscription.Create<PurchasesListed>(x => ListPurchases(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
        }

        private void NavigateBack()
        {
            RemoveCurrentPurchaseSummaries();
            _index -= _ordersPerPage;
            AddCurrentPurchaseSummaries();
        }

        private void NavigateForward()
        {
            RemoveCurrentPurchaseSummaries();
            _index += _ordersPerPage;
            RetrieveNeededPurchases();
            AddCurrentPurchaseSummaries();
        }

        private void RemoveCurrentPurchaseSummaries()
        {
            var currentlyViewingPurchases = GetCurrentlyViewingPurchases();
            currentlyViewingPurchases.ForEach(x => Branch.Remove(x.Branch));
        }

        private void AddCurrentPurchaseSummaries()
        {
            var currentlyViewingPurchases = GetCurrentlyViewingPurchases();
            currentlyViewingPurchases.ForEach(x => Branch.Add(x.Branch));
        }

        private void RetrieveNeededPurchases()
        {
            while (_purchaseUIs.Count < _index + _ordersPerPage && _purchaseSupplier.Current != null)
            {
                _purchaseSupplier.MoveNext();
                _purchaseUIs.Add(new NewPurchaseSummaryUI(_purchaseSupplier.Current, _grid.GetBlockSize(2, 1)));
            }
        }

        private void Inspect()
        {
            _isListing = false;
            Branch.ClearElements();
        }

        private void ListPurchases()
        {
            _isListing = true;
            _branches.ForEach(x => Branch.Add(x));
            AddCurrentPurchaseSummaries();
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isListing)
                return;
            Branch.ParentLocation = parentTransform.Location;
            _grid.Draw(parentTransform);
            var currentlyViewingPurchases = GetCurrentlyViewingPurchases();
            for (int i = 0; i < currentlyViewingPurchases.Count; i++)
                currentlyViewingPurchases[i].Draw(parentTransform + new Transform2(new Vector2(_grid.GetBlockSize(1, 1).Width, Sizes.Margin + i * (Sizes.PurchaseSummary.Height + Sizes.SmallMargin))));
        }

        public void EndCall()
        {
            RemoveCurrentPurchaseSummaries();
            _isListing = false;
            _index = 0;
            _purchaseUIs.Clear();
        }

        public void StartCall(Call call)
        {
            _purchaseSupplier = call.Scenario.Purchases.GetEnumerator();
            _purchaseSupplier.MoveNext();
            RetrieveNeededPurchases();
            AddCurrentPurchaseSummaries();
            _isListing = true;
        }

        private List<NewPurchaseSummaryUI> GetCurrentlyViewingPurchases()
        {
            return _purchaseUIs.GetRange(_index, MathHelper.Min(_ordersPerPage, _purchaseUIs.Count - _index));
        }
    }
}