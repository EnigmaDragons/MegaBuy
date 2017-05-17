using System;
using MegaBuy.Money.Amounts;

namespace MegaBuy.PurchaseHistories
{
    public class Purchase
    {
        public DateTime Date { get; }
        public string OrderID { get; }
        public string ProductID { get; }
        public string ProductName { get; }
        public string ProviderName { get; }
        public string ProviderID { get; }
        public IAmount TotalCost { get; }
        public IAmount ItemPrice { get; }
        public string ShippingAddress { get; }
        public string AddressOwner { get; }
        public bool isDelivered { get; }
        public string PromoCode { get; }
        public bool SoldAsIs { get; }
        public bool WasReturned { get; }
        public DateTime? DeliverDateTime { get; }
        public DateTime? ReturnDateTime { get; }
    }
}
