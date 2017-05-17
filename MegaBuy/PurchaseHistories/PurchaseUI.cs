using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseUI : IVisual
    {
        public PurchaseUI(Purchase purchase)
        {
            
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/purchase", Vector2.Zero);
        }
    }
}
