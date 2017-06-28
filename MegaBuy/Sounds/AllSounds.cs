using MegaBuy.Notifications;
using MegaBuy.Shopping.Foods;
using MonoDragons.Core.Audio;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Sounds
{
    public sealed class AllSounds
    {

        public AllSounds()
        {
            World.Subscribe(EventSubscription.Create<PlayerNotification>(PlayerNotificationReceived, this));
            World.Subscribe(EventSubscription.Create<FoodEaten>(FoodEaten, this));
        }

        private void FoodEaten(FoodEaten obj)
        {
            Audio.PlaySound("EatFood");
        }

        private void PlayerNotificationReceived(PlayerNotification obj)
        {
            Audio.PlaySound("PlayerNotification", 0.50f);
        }
    }
}
