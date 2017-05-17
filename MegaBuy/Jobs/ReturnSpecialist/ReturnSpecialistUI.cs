using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Jobs.ReturnSpecialist
{
    public class ReturnSpecialistUI : IVisualControl
    {
        private readonly ImageTextButton _purchaseHistory;

        private bool _isInCall = false;

        public ClickUIBranch Branch { get; }

        public ReturnSpecialistUI()
        {
            _purchaseHistory = ImageTextButtonFactory.Create("Purchases", new Vector2(700, 500), () => World.Publish(new AppChanged(App.PurchaseHistory)), () => _isInCall);
            Branch = new ClickUIBranch("Return Specialist", (int)ClickUIPriorities.Pad);
            Branch.Add(_purchaseHistory);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => _isInCall = true, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => _isInCall = false, this));
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.Location = parentTransform.Location;
            _purchaseHistory.Draw(parentTransform);
        }
    }
}
