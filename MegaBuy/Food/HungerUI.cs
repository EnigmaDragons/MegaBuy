using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Food
{
    public sealed class HungerUI : IVisual
    {
        private readonly ImageBox _icon;

        public HungerUI(ImageBox icon)
        {
            _icon = icon;
            World.Subscribe(new EventSubscription<NotHungry>(x => _icon.Clear(), this));
            World.Subscribe(new EventSubscription<Hungry>(x => _icon.SetImage("Images/Icons/food"), this));
            World.Subscribe(new EventSubscription<VeryHungry>(x => _icon.SetImage("Images/Icons/food-red"), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _icon.Draw(parentTransform);
        }
    }
}
