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
        private const string DateFormat = "yyyy/MM/dd";
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public PurchaseUI(Purchase purchase)
        {
            AddLabel(new Transform2(new Vector2(15, 15), new Size2(400, 25)), "14", purchase.ProductName ?? "null");
            AddLabel(new Transform2(new Vector2(425, 15), new Size2(400, 25)), "ten", "Product ID: " + purchase.ProductID ?? "null");
            AddLabel(new Transform2(new Vector2(835, 15), new Size2(400, 25)), "twelve", "Ordered On: " + purchase.Date.ToString(DateFormat));
            AddLabel(new Transform2(new Vector2(835, 45), new Size2(400, 25)), "ten", "Order ID: " + purchase.OrderID ?? "null");
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
