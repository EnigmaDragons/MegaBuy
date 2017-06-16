using System;
using System.Collections.Generic;
using System.Globalization;
using MegaBuy.PurchaseHistories.Data;
using MegaBuy.UIs;
using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories
{
    public class Purchase
    {
        private readonly DateTime _purchaseDate;
        private readonly decimal _itemPrice;

        public string Date { get; private set; }
        public string OrderID { get; private set; }
        public string ProductID { get; private set; }
        public string ProductName { get; private set; }
        public string ProductCategory { get; private set; }
        public string ProviderID { get; private set; }
        public string ProviderName { get; private set; }
        public string TotalCost { get; private set; }
        public string ItemPrice { get; private set; }
        public string ShippingAddress { get; private set; }
        public string AddressOwner { get; private set; }
        public bool IsDelivered { get; private set; }
        public string PromoCode { get; private set; }
        public bool SoldAsIs { get; private set; }
        public bool WasReturned { get; private set; }
        public string DeliverDateTime { get; private set; }
        public string ReturnDateTime { get; private set; }
        public int ItemsInStock { get; private set; }

        public Purchase(DateTime purchaseDate, decimal itemPrice)
        {
            _purchaseDate = purchaseDate;
            _itemPrice = itemPrice;
        }

        public bool PurchasedAfter(DateTime dt)
        {
            return _purchaseDate > dt;
        }

        public bool PurchasedWithinLast(int numDays)
        {
            return PurchasedAfter(CurrentGameState.State.DateTime.AddDays(-numDays));
        }

        public static IEnumerable<Purchase> CreateInfinite()
        {
            for (var date = CurrentGameState.State.DateTime; date > DateTime.MinValue.AddDays(1); date = date.AddHours(-Rng.Int(1, 20)))
                yield return Create(date);
        }

        public static IEnumerable<Purchase> CreateInfinite(DateTime lastDate)
        {
            for (var date = lastDate; date > DateTime.MinValue.AddDays(1); date = date.AddHours(-Rng.Int(1, 20))) 
                yield return Create(date);
        }

        public static IEnumerable<Purchase> CreateInfiniteWith(Purchase purchase)
        {
            bool displayedPurchase = false;
            for (var date = CurrentGameState.State.DateTime; date > DateTime.MinValue.AddDays(1); date = date.AddHours(-Rng.Int(1, 20)))
            {
                if (purchase._purchaseDate.Date.Equals(date.Date) && !displayedPurchase)
                {
                    displayedPurchase = true;
                    yield return purchase;
                }

                yield return CreateExcept(date, purchase.ProductName);
            }
        }

        public static Purchase CreateExcept(DateTime purchaseDate, string productName)
        {
            var wasSoldAsIs = Rng.Int(0, 100) < 16;
            var wasDelivered = Rng.Int(0, 100) < 98;
            var wasReturned = Rng.Int(0, 100) < 12;
            return Create(purchaseDate, Products.RandomExcept(productName), wasDelivered, wasSoldAsIs, wasReturned);
        }

        public static Purchase Create(DateTime purchaseDate)
        {
            return Create(purchaseDate, Products.Random);
        }

        public static Purchase Create(DateTime purchaseDate, Product product)
        {
            var wasSoldAsIs = Rng.Int(0, 100) < 16;
            var wasDelivered = Rng.Int(0, 100) < 98;
            var wasReturned = Rng.Int(0, 100) < 12;
            return Create(purchaseDate, Products.Random, wasDelivered, wasSoldAsIs, wasReturned);
        }

        public static Purchase Create(DateTime purchaseDate, Product product, bool wasDelivered, bool wasSoldAsIs, bool wasReturned)
        {
            var provider = Providers.Random;
            return new Purchase(purchaseDate, product.Price)
            {
                Date = purchaseDate.ToString(DateFormat.Get),
                OrderID = CreateId().RandomlyNullify(),
                ProductID = product.Id.RandomlyNullify(),
                ProductName = product.Name,
                ProductCategory = product.Category.ToString().RandomlyNullify(),
                ProviderID = provider.Id.RandomlyNullify(),
                ProviderName = provider.Name.RandomlyNullify(),
                PromoCode = PromoCodes.Random.RandomlyNullify(),
                SoldAsIs = wasSoldAsIs,
                WasReturned = wasReturned,
                ReturnDateTime = "NULL",
                AddressOwner = AddressOwners.Random.RandomlyNullify(),
                ItemPrice = product.Price.ToString(CultureInfo.InvariantCulture),
                TotalCost = (product.Price * Rng.Int(2, 5)).ToString(CultureInfo.InvariantCulture).RandomlyNullify(),
                ShippingAddress = ShippingAddresses.Random.RandomlyNullify(),
                DeliverDateTime = (wasDelivered ? purchaseDate.ToString(DateFormat.Get) : "NULL").RandomlyNullify(),
                IsDelivered = wasDelivered,
                ItemsInStock = Rng.Int(0, 2) * Rng.Int(1, 99999),
            };
        }

        private static string CreateId()
        {
            return Guid.NewGuid().ToString();
        }

        public bool PriceIsLessThan(int amount)
        {
            return _itemPrice < amount;
        }
    }
}
