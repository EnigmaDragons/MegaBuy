using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories.Data
{
    public static class ShippingAddresses
    {
        public static string Random => $"{Rng.Int()}.{Rng.Int()}.{Rng.Int()}";
    }
}
