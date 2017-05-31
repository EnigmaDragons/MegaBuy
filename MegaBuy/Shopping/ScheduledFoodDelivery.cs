using MegaBuy.Shopping.Foods;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Shopping
{
    public class ScheduledFoodDelivery
    {
        private int _minutesTilDelivery;
        private Food _food;

        public ScheduledFoodDelivery(int minutesTilDelivery, Food food)
        {
            _minutesTilDelivery = minutesTilDelivery;
            _food = food;
            World.Subscribe(EventSubscription.Create<MinuteChanged>((t) => MinuteChanged(), this));
        }

        private void MinuteChanged()
        {
            if(--_minutesTilDelivery == 0)
            {
                new FoodDelivered(_food);
                World.Unsubscribe(this);
            }
        }
    }
}