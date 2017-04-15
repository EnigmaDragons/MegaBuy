using System;
using MegaBuy.CustomUI;
using MegaBuy.Time;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class TimeUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.Margin, 800 - Sizes.Margin));
        private readonly Clock _clock;
        private readonly Label _label;

        public TimeUI()
        {
            _clock = GameState.Clock;
            _label = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(Sizes.Label) };
        }

        public void Update(TimeSpan delta)
        {
            _label.Text = _clock.Time;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label", parentTransform + new Transform2(_transform.Location, Sizes.Label));
            _label.Draw(parentTransform + _transform);
        }
    }
}
