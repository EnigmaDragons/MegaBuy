using System.Collections.Generic;
using MegaBuy.PurchaseHistories;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    public class NewPurchaseUI : IVisualControl
    {
        private readonly GridLayout _grid;
        private readonly ViewPurchaseListUI _viewList;

        public Transform2 Transform => new Transform2(_grid.Size);
        public ClickUIBranch Branch { get; }

        private bool _isShowing;

        public NewPurchaseUI(Size2 size)
        {
            Branch = new ClickUIBranch("Purchase UI", (int)ClickUIPriorities.Pad);
            _grid = new GridLayout(size - new Size2(Sizes.Margin * 2, Sizes.Margin * 2), 
                new List<Definition>
                {
                    new ConcreteDefinition(50),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ConcreteDefinition(50)
                }, 
                new List<Definition>
                {
                    new ShareDefintion(2),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ShareDefintion(),
                    new ConcreteDefinition(90)
                });
            _viewList = new ViewPurchaseListUI();
            Branch.Add(_viewList.Branch);
            World.Subscribe(EventSubscription.Create<PurchaseInspected>(x => OnPurchaseInspected(x.Purchase), this));
            World.Subscribe(EventSubscription.Create<PurchasesListed>(x => OnPurchasesListed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isShowing)
                _grid.Draw(parentTransform);
        }

        private void OnPurchaseInspected(Purchase purchase)
        {
            _grid.AddSpatial(new ImageBox(new Transform2(_grid.Size), "Images/UI/purchase"), new Transform2(_grid.Size), 1, 1, 5, 10);
            _isShowing = true;

            AddLabel(purchase.ProductName, "18", HorizontalAlignment.Center, 2, 1, 3, 1);

            AddLabel("Category: " + purchase.ProductCategory, "12", HorizontalAlignment.Left, 2, 2);
            AddLabel("Product ID: " + purchase.ProductID, "12", HorizontalAlignment.Left, 3, 2, 2, 1);

            AddLabel("Order Date: " + purchase.Date, "12", HorizontalAlignment.Left, 2, 3);
            AddLabel("Order ID: " + purchase.OrderID, "12", HorizontalAlignment.Left, 3, 3, 2, 1);

            var isDelivered = new ImageBox(new Transform2(new Vector2(-100, 0), new Size2(34, 34)), purchase.IsDelivered ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked");
            _grid.AddSpatial(isDelivered, isDelivered.Transform, 2, 4);
            AddLabel("Is Delivered", "14", HorizontalAlignment.Left, 2, 4, 1, 1, 50, 0);
            AddLabel("Date: " + purchase.DeliverDateTime, "14", HorizontalAlignment.Left, 4, 4);

            AddLabel("Provider: " + purchase.ProviderName, "12", HorizontalAlignment.Left, 2, 5, 2, 1);
            AddLabel("Provider ID: " + purchase.ProviderID, "12", HorizontalAlignment.Left, 4, 5);

            var wasReturned = new ImageBox(new Transform2(new Vector2(-100, 0), new Size2(34, 34)), purchase.WasReturned ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked");
            _grid.AddSpatial(wasReturned, wasReturned.Transform, 2, 6);
            AddLabel("Was Returned", "14", HorizontalAlignment.Left, 2, 6, 1, 1, 50, 0);
            AddLabel("Date: " + purchase.ReturnDateTime, "14", HorizontalAlignment.Left, 4, 6);

            AddLabel("Shipping Address: " + purchase.ShippingAddress + " Address Owner: " + purchase.AddressOwner, "14", HorizontalAlignment.Left, 2, 7, 3, 1);

            AddLabel("Promo Code: " + purchase.PromoCode, "14", HorizontalAlignment.Left, 2, 8, 3, 1);

            AddLabel("Item Cost: " + purchase.ItemPrice, "14", HorizontalAlignment.Left, 2, 9);
            AddLabel("Total Cost: " + purchase.TotalCost, "14", HorizontalAlignment.Left, 3, 9);

            var soldAsIs = new ImageBox(new Transform2(new Vector2(-100, 0), new Size2(34, 34)), purchase.SoldAsIs ? "Images/UI/checkbox-checked" : "Images/UI/checkbox-unchecked");
            _grid.AddSpatial(soldAsIs, soldAsIs.Transform, 3, 10);
            AddLabel("Sold As Is", "14", HorizontalAlignment.Left, 3, 10, 1, 1, 50, 0);
            AddLabel(purchase.ItemsInStock + " In Stock", "14", HorizontalAlignment.Left, 2, 10);
            _grid.AddSpatial(_viewList, _viewList.Transform, 4, 10, 2, 1);
        }

        private void OnPurchasesListed()
        {
            _isShowing = false;
            _grid.Clear();
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
