
using MegaBuy.Money.Amounts;

namespace MegaBuy.Money.Accounts
{
    public interface IAccount
    {
        void Add(IAmount amount);
        void Remove(IAmount amount);
    }
}
