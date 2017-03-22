
namespace MegaBuy.Money
{
    public interface IAccount
    {
        void Add(IAmount amount);
        void Remove(IAmount amount);
    }
}
