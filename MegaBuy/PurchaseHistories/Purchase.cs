using System;
using MegaBuy.Money.Amounts;

namespace MegaBuy.PurchaseHistories
{
    public class Purchase
    {
        public DateTime Date { get; set; } 
        public string OrderID { get; set; } 
        public string ProductID { get; set; } 
        public string ProductName { get; set; } 
        public string ProviderName { get; set; } 
        public string ProviderID { get; set; } 
        public IAmount TotalCost { get; set; } 
        public IAmount ItemPrice { get; set; } 
        public string ShippingAddress { get; set; } 
        public string AddressOwner { get; set; } 
        public bool IsDelivered { get; set; } 
        public string PromoCode { get; set; }
        public bool SoldAsIs { get; set; }
        public bool WasReturned { get; set; } 
        public DateTime? DeliverDateTime { get; set; } 
        public DateTime? ReturnDateTime { get; set; } 
    }
}
