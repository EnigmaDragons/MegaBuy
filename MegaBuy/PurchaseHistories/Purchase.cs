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
        
        public static IEnumerable<Purchase> CreateInfinite(DateTime lastDate)
        {
            for (var date = lastDate; date > DateTime.MinValue; date = date.AddHours(-Rng.Int(1, 20))) 
                yield return Create(date);
        }

        public static Purchase Create(DateTime purchaseDate)
        {
            var product = Products.Random;
            var provider = Providers.Random;
            var wasDelivered = Rng.Int(0, 100) < 98;
            return new Purchase
            {
                Date = purchaseDate.ToString(DateFormat.Get),
                OrderID = CreateId().RandomlyNullify(),
                ProductID = product.Id.RandomlyNullify(),
                ProductName = product.Name,
                ProductCategory = product.Category.ToString().RandomlyNullify(),
                ProviderID = provider.Id.RandomlyNullify(),
                ProviderName = provider.Name.RandomlyNullify(),
                PromoCode = PromoCodes.Random.RandomlyNullify(),
                SoldAsIs = Rng.Int(0, 100) < 33,
                WasReturned = Rng.Int(0, 100) < 12,
                ReturnDateTime = "NULL",
                AddressOwner = AddressOwners.Random.RandomlyNullify(),
                ItemPrice = product.Price.ToString(CultureInfo.InvariantCulture),
                TotalCost = (product.Price * Rng.Int(2, 5)).ToString(CultureInfo.InvariantCulture).RandomlyNullify(),
                ShippingAddress = ShippingAddresses.Random.RandomlyNullify(),
                DeliverDateTime = (wasDelivered ? purchaseDate.ToString(DateFormat.Get) : "NULL").RandomlyNullify(),
                IsDelivered = wasDelivered,
            };
        }

        private static string CreateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
