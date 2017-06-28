using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.BetweenCalls
{
    public class CallSummaryUI : ISpatialVisual
    {
        private readonly ImageLabel _label;

        private bool _technicalMistakeMade = false;
        private CallResolution _resolution;

        public Transform2 Transform { get; }

        public CallSummaryUI()
        {
            Transform = new Transform2(new Size2(600, Sizes.Label.Height));
            _label = new ImageLabel("", "Images/UI/label", Transform);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(x.Resolution), this));
            World.Subscribe(EventSubscription.Create<TechnicalMistakeOccurred>(x => OnTechnicalMistake(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _label.Draw(parentTransform);
        }

        private void OnCallStart()
        {
            _technicalMistakeMade = false;
        }

        private void OnCallResolved(CallResolution resolution)
        {
            _resolution = resolution;
            _label.Text = CallSummaries.GetSummary(_resolution, _technicalMistakeMade);
        }

        private void OnTechnicalMistake()
        {
            _technicalMistakeMade = true;
            _label.Text = CallSummaries.GetSummary(_resolution, _technicalMistakeMade);
        }
    }
}
