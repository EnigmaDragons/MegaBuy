using System.Collections.Generic;
using MegaBuy.PurchaseHistories;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    public class NewPurchaseSummaryUI : IVisualControl
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public ClickUIBranch Branch { get; }

        public NewPurchaseSummaryUI(Purchase purchase, Size2 size)
        {
            Branch = new ClickUIBranch("Purchase Summary", (int)ClickUIPriorities.Pad);
            var goToDetails = new ImageButton("Images/UI/purchase-summary-press", "Images/UI/purchase-summary", "Images/UI/purchase-summary-hover",
                new Transform2(new Size2(size.Width, 50)), () => World.Publish(new PurchaseInspected(purchase)));
            var name = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/14",
                Transform = new Transform2(new Vector2(10, 0), new Size2(size.Width - 20, 50)),
                HorizontalAlignment = HorizontalAlignment.Left,
                RawText = purchase.ProductName
            };
            var date = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/12",
                Transform = new Transform2(new Vector2(10, 0), new Size2(size.Width - 20, 50)),
                HorizontalAlignment = HorizontalAlignment.Right,
                RawText = purchase.Date
            };
            Branch.Add(goToDetails);
            _visuals.Add(goToDetails);
            _visuals.Add(name);
            _visuals.Add(date);
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
