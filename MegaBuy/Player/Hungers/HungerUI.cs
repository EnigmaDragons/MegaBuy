using MegaBuy.Player.Energy;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Player.Hungers
{
    public sealed class HungerUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(1600 - Sizes.SmallMargin - Sizes.OverlayIcon.Width, 900 - Sizes.SmallMargin - Sizes.OverlayIcon.Height));
        private string _hunger = "hunger-full";

        public HungerUI()
        {
            World.Subscribe(EventSubscription.Create<NotHungry>(x => _hunger = "hunger-full", this));
            World.Subscribe(EventSubscription.Create<Hungry>(x => _hunger = "hunger-half", this));
            World.Subscribe(EventSubscription.Create<VeryHungry>(x => _hunger = "hunger-empty", this));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/" + _hunger, parentTransform + _transform + new Transform2(Sizes.OverlayIcon));
        }
    }
}
