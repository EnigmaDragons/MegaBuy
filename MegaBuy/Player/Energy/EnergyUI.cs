using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Player.Energy
{
    public class EnergyUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.SmallMargin, 900 - Sizes.SmallMargin - Sizes.OverlayIcon.Height));
        private string _energy = "energy-full";

        public EnergyUI()
        {
            World.Subscribe(EventSubscription.Create<NotTired>(x => _energy = "energy-full", this));
            World.Subscribe(EventSubscription.Create<Tired>(x => _energy = "energy-half", this));
            World.Subscribe(EventSubscription.Create<VeryTired>(x => _energy = "energy-empty", this));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/" + _energy, parentTransform + _transform + new Transform2(Sizes.OverlayIcon));
        }
    }
}
