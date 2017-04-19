using MegaBuy.Money.Amounts;

namespace MegaBuy.Shopping
{
    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        IAmount Cost { get; }
    }
}
