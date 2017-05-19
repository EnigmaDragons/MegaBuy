using System.Collections.Generic;
using System.Drawing.Text;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseUI : IVisual
    {
        private const int ColumnWidth = 96;
        private const int RowHeight = 34;

        private readonly List<IVisual> _visuals = new List<IVisual>();

        public PurchaseUI(Purchase purchase)
        {
            AddLabel(0, 0, 5, 1, "16", HorizontalAlignment.Left, purchase.ProductName);
            AddLabel(0, 1, 5, 1, "10", HorizontalAlignment.Left, "Product ID: " + purchase.ProductID);
            AddLabel(0, 2, 5, 1, "14", HorizontalAlignment.Left, "Category: " + purchase.ProductCategory);
            AddLabel(0, 3, 5, 1, "12", HorizontalAlignment.Left, "Promo Code: " + purchase.PromoCode);
            AddLabel(5, 0, 5, 1, "12", HorizontalAlignment.Left, "Order Date: " + purchase.Date);
            AddLabel(5, 1, 5, 1, "10", HorizontalAlignment.Left, "Order ID: " + purchase.OrderID);
            AddLabel(5, 2, 5, 1, "12", HorizontalAlignment.Left, "Provider Name: " + purchase.ProviderName);
            AddLabel(5, 3, 5, 1, "10", HorizontalAlignment.Left, "Provider ID: " + purchase.ProviderID);
            AddLabel(10, 0, 4, 3, "12", HorizontalAlignment.Left, "Shipping Address: " + purchase.ShippingAddress);
            AddLabel(10, 3, 4, 1, "10", HorizontalAlignment.Left, "Address Owner: " + purchase.AddressOwner);
            AddLabel(0, 4, 34, 0, 2, 2, "14", HorizontalAlignment.Left, "Delivered");
            AddLabel(2, 4, 2, 2, "12", HorizontalAlignment.Left, "Delivered Date: " + purchase.DeliverDateTime);
            AddLabel(4, 4, 34, 0, 2, 2, "14", HorizontalAlignment.Left, "Returned");
            AddLabel(6, 4, 2, 2, "12", HorizontalAlignment.Left, "Returned Date: " + purchase.ReturnDateTime);
            AddLabel(8, 4, 34, 0, 2, 2, "14", HorizontalAlignment.Left, "Sold As Is");
            AddLabel(10, 4, 2, 2, "16", HorizontalAlignment.Left, "Item Price: " + purchase.ItemPrice);
            AddLabel(12, 4, 2, 2, "16", HorizontalAlignment.Left, "Total Cost: " + purchase.TotalCost);

            _visuals.Add(new ImageBox(new Transform2(new Vector2(5, 15 + RowHeight * 4), new Size2(1350, 10)), "Images/UI/line-vertical"));
            _visuals.Add(new ImageBox(new Transform2(new Vector2(15, (float)(15 + RowHeight * 4.5)), new Size2(34, 34)), purchase.IsDelivered ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked"));
            _visuals.Add(new ImageBox(new Transform2(new Vector2(15 + ColumnWidth * 4, (float)(15 + RowHeight * 4.5)), new Size2(34, 34)), purchase.WasReturned ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked"));
            _visuals.Add(new ImageBox(new Transform2(new Vector2(15 + ColumnWidth * 8, (float)(15 + RowHeight * 4.5)), new Size2(34, 34)), purchase.SoldAsIs ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked"));
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
