namespace MegaBuy.PurchaseHistories
{
    public class PurchaseInspected
    {
        public Purchase Purchase { get; }

        public PurchaseInspected(Purchase purchase)
        {
            Purchase = purchase;
        }
    }
}
