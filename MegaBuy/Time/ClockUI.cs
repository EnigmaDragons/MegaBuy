using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Time
{
    public sealed class ClockUI : IVisualAutomaton
    {
        private readonly Clock _clock;
        private readonly Label _label;

        public ClockUI(Clock clock, Label label)
        {
            _clock = clock;
            _label = label;
        }

        public void Update(TimeSpan delta)
        {
            _clock.Update(delta);
            _label.Text = _clock.Time;
        }

        public void Draw(Transform2 parentTransform)
        {
            _label.Draw(parentTransform);
        }
    }
}
