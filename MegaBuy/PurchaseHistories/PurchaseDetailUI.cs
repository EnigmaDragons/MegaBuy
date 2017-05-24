using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseDetailUI : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _parentBranch;
        private readonly ClickUIBranch _branch;

        private bool _isInspecting = false;
        private PurchaseUI _purchase;

        public PurchaseDetailUI(ClickUIBranch parentBranch)
        {
            _parentBranch = parentBranch;
            _branch = new ClickUIBranch("Purchase Detail", (int)ClickUIPriorities.Pad);
            var returnButton = ImageTextButtonFactory.Create("Return", new Vector2(1350, 650), Return);
            _visuals.Add(returnButton);
            _branch.Add(returnButton);
            World.Subscribe(EventSubscription.Create<PurchaseInspected>(x => Inspect(x.Purchase), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isInspecting)
                return;
            _branch.ParentLocation = parentTransform.Location;
            _visuals.ForEach(x => x.Draw(parentTransform));
            _purchase.Draw(parentTransform + new Transform2(new Vector2(120, 25)));
        }

        private void Inspect(Purchase purchase)
        {
            _isInspecting = true;
            _parentBranch.Add(_branch);
            _purchase = new PurchaseUI(purchase);
        }

        private void Return()
        {
            _isInspecting = false;
            _parentBranch.Remove(_branch);
            World.Publish(new PurchasesListed());
        }

        public void Dispose()
        {
            World.Unsubscribe(this);
        }
    }
}
