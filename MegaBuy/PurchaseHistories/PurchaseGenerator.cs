using MegaBuy.PurchaseHistories.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseGenerator
    {
        private bool _returnable;
        
        private int MaxDaysSinceBought = 90;
        private int MinDaysSinceBought = 0;
        private RestrictableBool SoldAsIs = new RestrictableBool();
        private RestrictableBool IsDelivered = new RestrictableBool();
        private decimal MaxPrice = decimal.MaxValue;
        private decimal MinPrice = 0;
        private RestrictableBool HasShippingAddress = new RestrictableBool();
        private List<ProductCategory> PossibleCategories = Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();

        public PurchaseGenerator(bool returnable)
        {
            _returnable = returnable;
        }

        /*public Optional<Purchase> Generate()
        {
            var product = Products.RandomInTheseCategoriesWithinPrice(PossibleCategories, MinPrice, MaxPrice);

            if (!product.HasValue || MinDaysSinceBought >= MaxDaysSinceBought)
                return new Optional<Purchase>();
            var delivered = IsDelivered.Value(0.98);
            var soldAsIs = SoldAsIs.Value(0.16);
            var hasShippingAddress = HasShippingAddress.Value(0.93);
            if (!delivered.HasValue || !soldAsIs.HasValue || !hasShippingAddress.HasValue)
                return new Optional<Purchase>();
            var date = DateBetween(MinDaysSinceBought, MaxDaysSinceBought);
            var purchase = Purchase.Create(date, product.Value, delivered.Value, soldAsIs.Value, Rng.Int(0, 100) < 12, ,,,,,);
            
        }*/

        private static DateTime DateBetween(int maxDaysEarlier, int minDaysEarlier)
        {
            return CurrentGameState.State.DateTime.AddDays(-Rng.Int(minDaysEarlier, maxDaysEarlier + 1));
        }

        /*Date = purchaseDate.ToString(DateFormat.Get),
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
                IsDelivered = wasDelivered,*/

        public void MustNotBeSoldAsIs()
        {
            SoldAsIs.Restrict(!_returnable);
        }

        public void MustBeSoldAsIs()
        {
            SoldAsIs.Restrict(_returnable);
        }

        public void MustNotBeDelivered()
        {
            IsDelivered.Restrict(!_returnable);
        }

        public void MustBeDelivered()
        {
            IsDelivered.Restrict(_returnable);
        }

        public void MustNotHaveShippingAddress()
        {
            HasShippingAddress.Restrict(!_returnable);
        }

        public void MustHaveShippingAddress()
        {
            HasShippingAddress.Restrict(_returnable);
        }

        public void MustBeAtMostXDaysSinceBought(int x)
        {
            if (_returnable)
                MaxDaysSinceBought = x;
            else
                MinDaysSinceBought = x;
        }

        public void MustBeAtLeastXDaysSinceBought(int x)
        {
            if (_returnable)
                MinDaysSinceBought = x;
            else
                MaxDaysSinceBought = x;
        }

        public void MustCostAtMost(decimal x)
        {
            if (_returnable)
                MaxPrice = x;
            else
                MinPrice = x;
        }

        public void MustCostAtLeast(decimal x)
        {
            if (_returnable)
                MinPrice = x;
            else
                MaxPrice = x;
        }
    }
}
