using MegaBuy.Money.Amounts;

namespace MegaBuy.Rents
{
    public interface IRent
    {
        void Increase(IAmount amount);
    }
}