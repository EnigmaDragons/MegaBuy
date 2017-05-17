using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseHistory : IApp
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        private readonly IEnumerator<Purchase> _purchaseSupplier;
        private readonly List<PurchaseUI> _purchases = new List<PurchaseUI>();
        private readonly int _ordersPerPage = 3;

        private int _index = 0;

        public App Type => App.PurchaseHistory;
        public ClickUIBranch Branch { get; }

        public PurchaseHistory()
            : this(Purchase.CreateInfinite(new DateTime(2020 + Rng.Int(1, 40), Rng.Int(1, 13), Rng.Int(1, 29)))) { }

        public PurchaseHistory(IEnumerable<Purchase> purchaseSupplier)
        {
            _purchaseSupplier = purchaseSupplier.GetEnumerator();
            Branch = new ClickUIBranch("Purchase History", (int)ClickUIPriorities.Pad);
            var backButton = ImageTextButtonFactory.CreateRotated("<<", new Vector2(Sizes.Margin, 275), NavigateBack, () => _index != 0);
            var forwardButton = ImageTextButtonFactory.CreateRotated(">>", new Vector2(1600 - Sizes.SideButton.Width - Sizes.Margin, 275), NavigateForward);
            var returnButton = ImageTextButtonFactory.Create("Return", new Vector2(1350, 650), () => World.Publish(new AppChanged(App.Call)));
            Branch.Add(backButton);
            Branch.Add(forwardButton);
            Branch.Add(returnButton);
            _visuals.Add(backButton);
            _visuals.Add(forwardButton);
            _visuals.Add(returnButton);
            RetrieveNeededPurchases();
        }

        private void NavigateBack()
        {
            _index -= _ordersPerPage;
        }

        private void NavigateForward()
        {
            _index += _ordersPerPage;
            RetrieveNeededPurchases();
        }

        private void RetrieveNeededPurchases()
        {
            while (_purchases.Count < _index + _ordersPerPage)
            {
                _purchaseSupplier.MoveNext();
                _purchases.Add(new PurchaseUI(_purchaseSupplier.Current));
            }
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
            var currentlyViewingPurchases = _purchases.GetRange(_index, _ordersPerPage);
            for (int i = 0; i < currentlyViewingPurchases.Count; i++)
                currentlyViewingPurchases[i].Draw(parentTransform + new Transform2(new Vector2(Sizes.Margin * 2 + Sizes.SideButton.Width, Sizes.Margin + i * (Sizes.Purchase.Height + Sizes.Margin))));
        }
    }
}
