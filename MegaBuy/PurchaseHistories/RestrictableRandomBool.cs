using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories
{
    public class RestrictableBool
    {
        private bool CanBeTrue = true;
        private bool CanBeFalse = true;

        public RestrictableBool()
        {
        }

        public void Restrict(bool isTrue)
        {
            if (isTrue)
                CanBeFalse = false;
            else
                CanBeTrue = false;
        }

        public Optional<bool> Value(double chanceOfTrue = 0.5d)
        {
            if (CanBeTrue && CanBeFalse)
                return new Optional<bool>(Rng.Dbl() < chanceOfTrue);
            if (CanBeTrue)
                return new Optional<bool>(true);
            if (CanBeFalse)
                return new Optional<bool>(false);
            return new Optional<bool>();
        }
    }
}