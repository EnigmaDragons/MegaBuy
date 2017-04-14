using System;
using MegaBuy.Apps;
using MegaBuy.Calls;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp.UIThings
{
    public class CallAppUI : IApp
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly CallMessengerUI _messenger = new CallMessengerUI();
        private readonly CallerUI _caller = new CallerUI();
        private readonly ReadyUI _ready = new ReadyUI();

        public ClickUIBranch Branch { get; private set; }
        public App Type => App.Call;

        public CallAppUI()
        {
            Branch = new ClickUIBranch("Call App", (int)ClickUIPriorities.Pad);
            World.Subscribe(EventSubscription.Create<CallConnecting>(x => StartConnecting(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
        }

        public void Update(TimeSpan delta)
        {
            _messenger.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _messenger.Draw(absoluteTransform);
            _caller.Draw(absoluteTransform);
            _ready.Draw(parentTransform);
        }

        private void StartConnecting()
        {
            Branch.Remove(_ready.Branch);
        }

        private void EndCall()
        {
            Branch.Add(_ready.Branch);
        }
    }
}
