using System.Collections.Generic;
using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.Messages
{
    public class ExcusesUI : IVisualControl
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ImageTextButton _excuseButton;

        public ClickUIBranch Branch { get; }
        public Transform2 Transform => _excuseButton.Transform;

        public ExcusesUI()
        {
            Branch = new ClickUIBranch("Excuses", (int)ClickUIPriorities.Pad);
            _excuseButton = ImageTextButtonFactory.Create("Excuse", new Vector2(0, 0), () => { });
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        private void OnCallStart()
        {
            _visuals.Add(_excuseButton);
            Branch.Add(_excuseButton);
        }

        private void OnCallResolved()
        {
            _visuals.Remove(_excuseButton);
            Branch.Remove(_excuseButton);
        }
    }
}
