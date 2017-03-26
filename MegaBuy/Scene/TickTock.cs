using System;
using MegaBuy.Time;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public sealed class TickTock : IScene
    {
        private Label _clockText;
        private Clock _clock;

        public void Init()
        {
            _clockText = new Label(Color.White);
            _clock = new Clock(_clockText);
        }

        public void Update(TimeSpan delta)
        {
            _clock.Update(delta);
        }

        public void Draw()
        {
            _clockText.Draw(Transform2.Zero);
        }
    }
}
