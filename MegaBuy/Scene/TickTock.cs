using System;
using MegaBuy.Apartment;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace MegaBuy.Scene
{
    public sealed class TickTock : IScene
    {
        private MutableDrawnText _clockText;
        private Clock _clock;

        public void Init()
        {
            _clockText = new MutableDrawnText(Color.White);
            _clock = new Clock(_clockText);
        }

        public void Update(TimeSpan delta)
        {
            _clock.Update(delta);
        }

        public void Draw()
        {
            _clockText.Draw(Transform.Zero);
        }
    }
}
