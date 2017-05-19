using System;
using System.Collections.Generic;
using System.Globalization;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.PurchaseHistories.Data;
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

        private const string DateFormat = "yyyy/MM/dd";

        public static IEnumerable<Purchase> CreateInfinite(DateTime lastDate)
        {
            for (var date = lastDate; date > DateTime.MinValue; date = date.AddHours(-Rng.Int(1, 20))) 
                yield return Create(date);
        }

        public static Purchase Create(DateTime purchaseDate)
        {
            var provider = Providers.Random;
            var price = 100.00m;
            return new Purchase
            {
                Date = purchaseDate.ToString(DateFormat),
                OrderID = CreateId().RandomlyNullify(),
                ProductID = CreateId().RandomlyNullify(),
                ProductName = Products.Random,
                ProviderID = provider.Id.RandomlyNullify(),
                ProviderName = provider.Name.RandomlyNullify(),
                PromoCode = PromoCodes.Random.RandomlyNullify(),
                SoldAsIs = Rng.Bool(),
                WasReturned = false,
                ReturnDateTime = "NULL",
                // @todo #1 Refine the following data values
                ProductCategory = "NULL",
                ItemPrice = price.ToString(CultureInfo.InvariantCulture),
                TotalCost = (price * Rng.Int(1, 5)).ToString(CultureInfo.InvariantCulture).RandomlyNullify(),
                ShippingAddress = "NULL",
                AddressOwner = "NULL",
                DeliverDateTime = purchaseDate.ToString(DateFormat),
                IsDelivered = true,
            };
        }

        private static string CreateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
