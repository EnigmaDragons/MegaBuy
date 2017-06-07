using System;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.ReturnCalls.PurchaseHistories;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.Callers
{
    public class PurchaseHistoryNavigationUI : ISpatialVisualControl
    {
        private readonly ImageTextButton _button;

        private bool _isCalling = false;

        public Transform2 Transform => _button.Transform;
        public ClickUIBranch Branch { get; }

        public PurchaseHistoryNavigationUI()
        {
            Branch = new ClickUIBranch("Purchase History Button", (int)ClickUIPriorities.Pad);
            _button = ImageTextButtonFactory.Create("Purchases", Vector2.Zero, () => World.Publish(new PurchaseHistoryViewed()));
            Branch.Add(_button);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isCalling)
                return;
            Branch.ParentLocation = parentTransform.Location;
            _button.Draw(parentTransform);
        }

        private void OnCallStart()
        {
            _isCalling = true;
            Branch.Add(_button);
        }

        private void OnCallResolved()
        {
            _isCalling = false;
            Branch.Remove(_button);
        }
    }
}
