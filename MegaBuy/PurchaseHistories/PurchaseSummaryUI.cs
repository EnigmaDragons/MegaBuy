using System.Collections.Generic;
using System.Windows.Media;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using Color = Microsoft.Xna.Framework.Color;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseSummaryUI : IVisualControl
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public ClickUIBranch Branch { get; }

        public PurchaseSummaryUI(Purchase purchase)
        {
            Branch = new ClickUIBranch("Purchase Summary", (int)ClickUIPriorities.Pad);
            var goToDetails = new ImageButton("Images/UI/purchase-summary-press", "Images/UI/purchase-summary", "Images/UI/purchase-summary-hover", 
                new Transform2(new Vector2(120, 0), new Size2(1360, 50)), () => World.Publish(new PurchaseInspected(purchase)));
            var name = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/14",
                Transform = new Transform2(new Vector2(145, 0), new Size2(655, 50)),
                HorizontalAlignment = HorizontalAlignment.Left,
                RawText = purchase.ProductName
            };
            var date = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/12",
                Transform = new Transform2(new Vector2(800, 0), new Size2(655, 50)),
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
