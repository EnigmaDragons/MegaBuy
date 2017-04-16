using System;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Ratings;
using MegaBuy.Pads.Apps;
using MegaBuy.Temp;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Calls
{
    public class CallApp : IApp
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly CallMessengerUI _messenger = new CallMessengerUI();
        private readonly CallerUi _caller = new CallerUi();
        private readonly ReadyUI _ready = new ReadyUI();
        private readonly CallOptionsUI _callOptions = new CallOptionsUI();
        private readonly RatingUI _rating = new RatingUI();

        private bool _isCalling = false;

        public ClickUIBranch Branch { get; private set; }
        public App Type => App.Call;

        public CallApp()
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
            {
                _ready.Draw(absoluteTransform);
                _rating.Draw(absoluteTransform);
            }
        }

        private void StartConnecting()
        {
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
