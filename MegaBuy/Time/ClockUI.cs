using System;
using MegaBuy.Temp;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Time
{
    public class ClockUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.SmallMargin + Sizes.OverlayIcon.Width + Sizes.Margin, 900 - Sizes.SmallMargin - Sizes.SmallLabel.Height));
        private readonly Clock _clock;
        private readonly Label _label;
        // @todo #1 Display DateTime in ClockUI

        public ClockUI()
        {
            _clock = CurrentGameState.State.Clock;
            _label = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(Sizes.SmallLabel) };
        }

        public void Update(TimeSpan delta)
        {
            _label.Text = _clock.Time;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label-small", parentTransform + new Transform2(_transform.Location, Sizes.SmallLabel));
            _label.Draw(parentTransform + _transform);
        }
    }
}
