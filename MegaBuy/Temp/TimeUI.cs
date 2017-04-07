using System;
using MegaBuy.Time;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class TimeUI : IVisualAutomaton
    {
        private readonly Vector2 _location = new Vector2(0, 850);
        private readonly Clock _clock;
        private readonly Label _label;

        public TimeUI(Clock clock)
        {
            _clock = clock;
            _label = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(new Size2(200, 50)) };
        }

        public void Update(TimeSpan delta)
        {
            _label.Text = _clock.Time;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label", parentTransform + new Transform2(_location, new Size2(200, 50)));
            _label.Draw(parentTransform + new Transform2(_location));
        }
    }
}
