using MegaBuy.Shopping.Foods;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Shopping
{
    public class FoodDelivery
    {
        public FoodDelivery()
        {
            World.Subscribe(EventSubscription.Create<FoodOrdered>(Ordered, this));
        }

        private void Ordered(FoodOrdered ordered)
        {
            new ScheduledFoodDelivery(Rng.Int(10, 21), ordered.Food);
        }
    }
}
