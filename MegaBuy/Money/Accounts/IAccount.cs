
using MegaBuy.Money.Amounts;

namespace MegaBuy.Money.Accounts
{
    public interface IAccount
    {
        void PaySalary(IAmount amount);
        void Add(IAmount amount);
        void Remove(IAmount amount);
    }
}
