using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.Callers
{
    public class CallerNameUI : ISpatialVisual
    {
        private readonly ImageLabel _name;

        public Transform2 Transform { get; }

        public CallerNameUI()
        {
            var transform = new Transform2(new Size2(250, 50));
            Transform = transform;
            _name = new ImageLabel("", "Images/UI/call-detail", Transform);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _name.Draw(parentTransform);
        }

        private void OnCallStart(Call call)
        {
            _name.Text = call.Caller.Name;
        }

        private void OnCallResolved()
        {
            _name.Text = "";
        }
    }
}
