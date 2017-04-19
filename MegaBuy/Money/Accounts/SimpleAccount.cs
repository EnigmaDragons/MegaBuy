
using System;
using MegaBuy.Money.Amounts;

namespace MegaBuy.Money.Accounts
{
    public abstract class SimpleAccount : IAccount, IAmount
    {
        private decimal _amount;
        
        protected SimpleAccount(decimal initial)
        {
            _amount = initial;
        }

        public void PaySalary(IAmount amount)
        {
            _amount += amount.Amount();
        }

        public void Add(IAmount amount)
        {
            _amount += amount.Amount();
        }

        public void Remove(IAmount amount)
        {
            _amount -= amount.Amount();
        }

        public decimal Amount()
        {
            return _amount;
        }

        
    }
}
