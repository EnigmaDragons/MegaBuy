using MegaBuy.Calls.Events;
using MegaBuy.PurchaseHistories;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    public class ViewPurchaseListUI : ISpatialVisualControl
    {
        private readonly ImageTextButton _button;

        private bool _isViewingList = true;

        public Transform2 Transform => _button.Transform;
        public ClickUIBranch Branch { get; }

        public ViewPurchaseListUI()
        {
            Branch = new ClickUIBranch("View List Button", (int)ClickUIPriorities.Pad);
            _button = ImageTextButtonFactory.Create("Purchases", Vector2.Zero, () => World.Publish(new PurchasesListed()));
            Branch.Add(_button);
            World.Subscribe(EventSubscription.Create<PurchaseInspected>(x => OnPurchaseInspected(), this));
            World.Subscribe(EventSubscription.Create<PurchasesListed>(x => OnPurchasesListed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isViewingList)
                return;
            Branch.ParentLocation = parentTransform.Location;
            _button.Draw(parentTransform);
        }

        private void OnPurchaseInspected()
        {
            _isViewingList = false;
            Branch.Add(_button);
        }

        private void OnPurchasesListed()
        {
            _isViewingList = true;
            Branch.Remove(_button);
        }
    }
}
