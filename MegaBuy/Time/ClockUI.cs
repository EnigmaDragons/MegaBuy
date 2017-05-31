using System;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Time
{
    public class ClockUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.SmallMargin + Sizes.OverlayIcon.Width + Sizes.LargeMargin, 900 - Sizes.SmallMargin - Sizes.SmallLabel.Height));
        private readonly Vector2 _datePosition = new Vector2(175, 0);
        private readonly Clock _clock;
        private readonly Label _time;
        private readonly Label _date;

        public ClockUI()
        {
            _clock = CurrentGameState.State.Clock;
            _time = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(Sizes.SmallLabel) };
            _date = new Label { BackgroundColor = Color.Transparent, TextColor = Color.White, Transform = new Transform2(Sizes.MediumLabel) };
        }

        public void Update(TimeSpan delta)
        {
            _time.Text = _clock.Time;
            _date.Text = _clock.Date;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/label-small", parentTransform + new Transform2(_transform.Location, Sizes.SmallLabel));
            _time.Draw(parentTransform + _transform);
            World.Draw("Images/UI/label-small", parentTransform + new Transform2(_transform.Location + _datePosition, Sizes.MediumLabel));
            _date.Draw(parentTransform + _transform + _datePosition);
        }
    }
}
