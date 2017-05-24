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
        private const int ColumnWidth = 440;
        private const int RowHeight = 35;

        private readonly List<IVisual> _visuals = new List<IVisual>();

        public PurchaseUI(Purchase purchase)
        {
            AddLabel(0, 0, 3, 2, "18", HorizontalAlignment.Center, purchase.ProductName);
            AddLabel(0, 2, 3, 1, "12", HorizontalAlignment.Center, "Product ID: " + purchase.ProductID);
            AddLabel(0, 3, 3, 1, "16", HorizontalAlignment.Center, "Category: " + purchase.ProductCategory);

            AddLabel(0, 5, 1, 1, "16", HorizontalAlignment.Left, "Order Date: " + purchase.Date);
            AddLabel(0, 6, 1, 1, "12", HorizontalAlignment.Left, "Order ID: " + purchase.OrderID);

            AddLabel(0, 9, 34, 0, 1, 1, "16", HorizontalAlignment.Left, "Returned");
            AddLabel(0, 10, 1, 1, "14", HorizontalAlignment.Left, "Returned Date: " + purchase.ReturnDateTime);
            _visuals.Add(new ImageBox(new Transform2(new Vector2(15, (float)(15 + RowHeight * 9)), new Size2(34, 34)), purchase.WasReturned ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked"));

            AddLabel(0, 13, 34, 0, 1, 2, "16", HorizontalAlignment.Left, "Sold As Is");
            _visuals.Add(new ImageBox(new Transform2(new Vector2(15, (float)(15 + RowHeight * 13.5)), new Size2(34, 34)), purchase.SoldAsIs ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked"));

            AddLabel(1, 5, 1, 1, "14", HorizontalAlignment.Left, "Shipping Address: " + purchase.ShippingAddress);
            AddLabel(1, 6, 1, 1, "12", HorizontalAlignment.Left, "Address Owner: " + purchase.AddressOwner);

            AddLabel(1, 9, 34, 0, 1, 1, "16", HorizontalAlignment.Left, "Delivered");
            AddLabel(1, 10, 1, 1, "14", HorizontalAlignment.Left, "Delivered Date: " + purchase.DeliverDateTime);
            _visuals.Add(new ImageBox(new Transform2(new Vector2(15 + ColumnWidth, (float)(15 + RowHeight * 9)), new Size2(34, 34)), purchase.IsDelivered ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked"));

            AddLabel(2, 5, 1, 1, "14", HorizontalAlignment.Left, "Provider Name: " + purchase.ProviderName);
            AddLabel(2, 6, 1, 1, "12", HorizontalAlignment.Left, "Provider ID: " + purchase.ProviderID);

            AddLabel(2, 9, 1, 2, "14", HorizontalAlignment.Left, "Promo Code: " + purchase.PromoCode);

            AddLabel(2, 12, 1, 2, "18", HorizontalAlignment.Left, "Item Price: " + purchase.ItemPrice);
            AddLabel(2, 14, 1, 2, "18", HorizontalAlignment.Left, "Total Cost: " + purchase.TotalCost);
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/purchase", parentTransform + new Transform2(Sizes.Purchase));
            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        private void AddLabel(int column, int row, int columnSpan, int rowSpan, string font, HorizontalAlignment horizontalAlignment, string text)
        {
            AddLabel(column, row, 0, 0, columnSpan, rowSpan, font, horizontalAlignment, text);
        }

        private void AddLabel(int column, int row, int xOffset, int yOffset, int columnSpan, int rowSpan, string font, HorizontalAlignment horizontalAlignment, string text)
        {
            _visuals.Add(new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Transform = new Transform2(new Vector2(15 + ColumnWidth * column + xOffset, 15 + RowHeight * row + yOffset), new Size2(ColumnWidth * columnSpan - xOffset, RowHeight * rowSpan - yOffset)),
                Font = "Fonts/" + font,
                Text = text,
                HorizontalAlignment = horizontalAlignment,
            });
        }
    }
}
