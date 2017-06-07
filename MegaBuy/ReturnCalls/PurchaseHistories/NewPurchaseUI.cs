using MegaBuy.PurchaseHistories;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    public class NewPurchaseUI : IVisual
    {
        private readonly GridLayout _grid;

        public NewPurchaseUI(Size2 size)
        {
            _grid = new GridLayout(size, 2, 18);
            World.Subscribe(EventSubscription.Create<PurchaseInspected>(x => OnPurchaseInspected(x.Purchase), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _grid.Draw(parentTransform);
        }

        private void OnPurchaseInspected(Purchase purchase)
        {
            _grid.Clear();
            _grid.AddSpatial(new ImageBox(new Transform2(_grid.Size), "Images/UI/purchase"), new Transform2(_grid.Size), 1, 1, 2, 18);

            AddLabel(purchase.ProductName, "16", HorizontalAlignment.Center, 1, 1, 2, 1);
            AddLabel("Product ID: " + purchase.ProductID, "10", HorizontalAlignment.Center, 1, 2, 2, 1);
            AddLabel("Category: " + purchase.ProductCategory, "12", HorizontalAlignment.Center, 1, 3);
            AddLabel("Order Date: " + purchase.Date, "14", HorizontalAlignment.Center, 2, 3);
            AddLabel("Order ID: " + purchase.OrderID, "10", HorizontalAlignment.Center, 1, 4, 2, 1);

            AddLabel("Provider: " + purchase.ProviderName, "12", HorizontalAlignment.Center, 1, 6, 2, 1);
            AddLabel("Provider ID: " + purchase.ProviderID, "10", HorizontalAlignment.Center, 1, 7, 2, 1);

            AddLabel("Shipping Address: " + purchase.ShippingAddress, "12", HorizontalAlignment.Center, 1, 9, 2, 1);
            AddLabel("Address Owner: " + purchase.AddressOwner, "12", HorizontalAlignment.Center, 1, 10, 2, 1);

            var isDelivered = new ImageBox(new Transform2(new Vector2(-100, 0), new Size2(34, 34)), purchase.IsDelivered ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked");
            _grid.AddSpatial(isDelivered, isDelivered.Transform, 1, 11);
            AddLabel("Is Delivered", "14", HorizontalAlignment.Center, 1, 11);
            AddLabel("Date: " + purchase.DeliverDateTime, "14", HorizontalAlignment.Center, 2, 11);

            var wasReturned = new ImageBox(new Transform2(new Vector2(-100, 0), new Size2(34, 34)), purchase.WasReturned ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked");
            _grid.AddSpatial(wasReturned, wasReturned.Transform, 1, 13);
            AddLabel("Was Returned", "14", HorizontalAlignment.Center, 1, 13);
            AddLabel("Date: " + purchase.ReturnDateTime, "14", HorizontalAlignment.Center, 2, 13);

            var soldAsIs = new ImageBox(new Transform2(new Vector2(-100, 0), new Size2(34, 34)), purchase.SoldAsIs ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked");
            _grid.AddSpatial(soldAsIs, soldAsIs.Transform, 1, 15);
            AddLabel("Sold As Is", "14", HorizontalAlignment.Center, 1, 15);
            AddLabel("Promo Code: " + purchase.PromoCode, "14", HorizontalAlignment.Center, 2, 15);

            AddLabel("Item Cost: " + purchase.ItemPrice, "14", HorizontalAlignment.Center, 1, 17);
            AddLabel("Total Cost: " + purchase.TotalCost, "14", HorizontalAlignment.Center, 2, 17);
        }

        private void AddLabel(string text, string font, HorizontalAlignment horizontalAlignment, int column, int row, int columnSpan = 1, int rowSpan = 1, int xOffset = 0, int yOffset = 0)
        {
            var label = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Transform = new Transform2(new Vector2(xOffset / 2, yOffset / 2), _grid.GetBlockSize(column, row, columnSpan, rowSpan) - new Size2(xOffset, yOffset)),
                Font = "Fonts/" + font,
                Text = text,
                HorizontalAlignment = horizontalAlignment,
            };
            _grid.AddSpatial(label, label.Transform, column, row, columnSpan, rowSpan);
        }
    }
}
