using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.Ready
{
    public class ReturnsCallReadyUI : ISpatialVisualControl
    { 
        private readonly ImageTextButton _button;

        private bool _isCalling = false;

        public Transform2 Transform => _button.Transform;
        public ClickUIBranch Branch { get; }

        public ReturnsCallReadyUI()
        {
            Branch = new ClickUIBranch("Ready Button", (int)ClickUIPriorities.Pad);
            _button = ImageTextButtonFactory.Create("Ready", Vector2.Zero, StartCall);
            Branch.Add(_button);
            World.Subscribe(EventSubscription.Create<CallResolved>(x => AtEndCall(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isCalling)
                return;
            Branch.ParentLocation = parentTransform.Location;
            _button.Draw(parentTransform);
        }

        private void StartCall()
        {
            World.Publish(new AgentCallStatusChanged(AgentCallStatus.Available));
            _isCalling = true;
            Branch.Remove(_button);
        }

        private void AtEndCall()
        {
            World.Publish(new AgentCallStatusChanged(AgentCallStatus.Idle));
            _isCalling = false;
            Branch.Add(_button);
        }
    }
}
