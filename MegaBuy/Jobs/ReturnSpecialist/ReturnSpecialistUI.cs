using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Jobs.ReturnSpecialist
{
    public class ReturnSpecialistUI : IVisualControl
    {
        private readonly ImageTextButton _purchaseHistory;

        public ClickUIBranch Branch { get; }

        public ReturnSpecialistUI()
        {
            _purchaseHistory = ImageTextButtonFactory.Create("Purchases", new Vector2(700, 500), () => World.Publish(new AppChanged(App.PurchaseHistory)));
            Branch = new ClickUIBranch("Return Specialist", (int)ClickUIPriorities.Pad);
            Branch.Add(_purchaseHistory);
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.Location = parentTransform.Location;
            _purchaseHistory.Draw(parentTransform);
        }
    }
}
