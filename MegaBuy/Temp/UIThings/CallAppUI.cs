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
        private readonly CallOptionsUI _callOptions = new CallOptionsUI();

        private bool _isCalling = false;

        public ClickUIBranch Branch { get; private set; }
        public App Type => App.Call;

        public CallAppUI()
        {
            Branch = new ClickUIBranch("Call App", (int)ClickUIPriorities.Pad);
            Branch.Add(_ready.Branch);
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
            _callOptions.Draw(absoluteTransform);
            if (!_isCalling)
                _ready.Draw(absoluteTransform);
        }

        private void StartConnecting()
        {
            // @todo #1 Need Call Connecting published before call after of call
            _isCalling = true;
            Branch.Remove(_ready.Branch);
            Branch.Add(_callOptions.Branch);
        }

        private void EndCall()
        {
            _isCalling = false;
            Branch.Remove(_callOptions.Branch);
            Branch.Add(_ready.Branch);
        }
    }
}
