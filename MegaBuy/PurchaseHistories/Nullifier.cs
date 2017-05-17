using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories
{
    public static class Nullifier
    {
        private const int ChanceOfBeingNull = 15;

        private static bool ShouldReturnNull()
        {
            return Rng.Int(ChanceOfBeingNull) == 0;
        }

        public static string RandomlyNullify(this string src)
        {
            return ShouldReturnNull() ? "NULL" : src;
        }
    }
}
