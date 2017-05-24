﻿using System;
using System.Collections.Generic;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class PurchasesUI : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        private readonly IEnumerator<Purchase> _purchaseSupplier;
        private readonly List<PurchaseSummaryUI> _purchaseUIs = new List<PurchaseSummaryUI>();
        private readonly int _ordersPerPage = 10;
        private readonly ClickUIBranch _parentBranch;
        private readonly ClickUIBranch _branch;

        private int _index = 0;
        private bool _isListing = true;

        public PurchasesUI(ClickUIBranch parentBranch)
            : this(Purchase.CreateInfinite(new DateTime(2020 + Rng.Int(1, 40), Rng.Int(1, 13), Rng.Int(1, 29))), parentBranch) { }

        public PurchasesUI(IEnumerable<Purchase> purchaseSupplier, ClickUIBranch parentBranch)
        {
            _parentBranch = parentBranch;
            _purchaseSupplier = purchaseSupplier.GetEnumerator();
            _branch = new ClickUIBranch("Purchases", (int)ClickUIPriorities.Pad);
            _parentBranch.Add(_branch);
            var backButton = ImageTextButtonFactory.CreateRotated("<<", new Vector2(Sizes.Margin, 275), NavigateBack, () => _index != 0);
            var forwardButton = ImageTextButtonFactory.CreateRotated(">>", new Vector2(1600 - Sizes.SideButton.Width - Sizes.Margin, 275), NavigateForward);
            var returnButton = ImageTextButtonFactory.Create("Return", new Vector2(1350, 650), () => World.Publish(new AppChanged(App.Call)));
            _branch.Add(backButton);
            _branch.Add(forwardButton);
            _branch.Add(returnButton);
            _visuals.Add(backButton);
            _visuals.Add(forwardButton);
            _visuals.Add(returnButton);
            World.Subscribe(EventSubscription.Create<PurchaseInspected>(x => Inspect(), this));
            World.Subscribe(EventSubscription.Create<PurchasesListed>(x => ListPurchases(), this));
            RetrieveNeededPurchases();
            AddCurrentPurchaseSummaries();
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
            var currentlyViewingPurchases = _purchaseUIs.GetRange(_index, _ordersPerPage);
            currentlyViewingPurchases.ForEach(x => _branch.Remove(x.Branch));
        }

        private void AddCurrentPurchaseSummaries()
        {
            var currentlyViewingPurchases = _purchaseUIs.GetRange(_index, _ordersPerPage);
            currentlyViewingPurchases.ForEach(x => _branch.Add(x.Branch));
        }

        private void RetrieveNeededPurchases()
        {
            while (_purchaseUIs.Count < _index + _ordersPerPage)
            {
                _purchaseSupplier.MoveNext();
                _purchaseUIs.Add(new PurchaseSummaryUI(_purchaseSupplier.Current));
            }
        }

        private void Inspect()
        {
            _isListing = false;
            _parentBranch.Remove(_branch);
        }

        private void ListPurchases()
        {
            _isListing = true;
            _parentBranch.Add(_branch);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isListing)
                return;
            _branch.ParentLocation = parentTransform.Location;
            _visuals.ForEach(x => x.Draw(parentTransform));
            var currentlyViewingPurchases = _purchaseUIs.GetRange(_index, _ordersPerPage);
            for (int i = 0; i < currentlyViewingPurchases.Count; i++)
                currentlyViewingPurchases[i].Draw(parentTransform + new Transform2(new Vector2(0, Sizes.Margin + i * (Sizes.PurchaseSummary.Height + Sizes.SmallMargin))));
        }

        public void Dispose()
        {
            World.Unsubscribe(this);
        }
    }
}
