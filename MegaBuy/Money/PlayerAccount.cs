using MonoDragons.Core.Engine;

namespace MegaBuy.Money
{
    public sealed class PlayerAccount : IAccount, IAmount
    {
        private decimal _amount;

        public PlayerAccount(decimal initial)
        {
            _amount = initial;
        }

        public void Add(IAmount amount)
        {
            _amount += amount.Amount();
            World.Publish(new MoneyDeposited());
        }

        public void Remove(IAmount amount)
        {
            throw new System.NotImplementedException();
        }

        public decimal Amount()
        {
            return _amount;
        }
    }
}
