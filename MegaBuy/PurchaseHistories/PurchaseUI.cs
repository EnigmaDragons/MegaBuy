using System.Collections.Generic;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseUI : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public PurchaseUI(Purchase purchase)
        {
            AddLabel(new Transform2(new Vector2(15, 15), new Size2(670, 25)), "14", purchase.ProductName ?? "null");
            AddLabel(new Transform2(new Vector2(680, 15), new Size2(335, 25)), "ten", "Product ID: " + purchase.ProductID ?? "null");
            AddLabel(new Transform2(new Vector2(1020, 15), new Size2(330, 25)), "twelve", "Ordered On: " + purchase.Date);
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/purchase", parentTransform + new Transform2(Sizes.Purchase));
            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        private void AddLabel(Transform2 transform, string font, string text)
        {
            _visuals.Add(new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Transform = transform,
                Font = "Fonts/" + font,
                Text = text
            });
        }
    }
}
