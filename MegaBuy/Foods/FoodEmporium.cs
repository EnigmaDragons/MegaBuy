using MegaBuy.Money.Accounts;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Foods
{
    public sealed class FoodEmporium
    {
        private readonly IAccount _playerAccount;

        public FoodEmporium(IAccount playerAccount)
        {
            _playerAccount = playerAccount;
            World.Subscribe(EventSubscription.Create<FoodOrdered>(FoodOrdered, this));
        }

        private void FoodOrdered(FoodOrdered order)
        {
            _playerAccount.Remove(order.Food.Cost);
        }
    }
}
