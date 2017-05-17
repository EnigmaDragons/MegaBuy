﻿using System;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Options;
using MegaBuy.Calls.Ratings;
using MegaBuy.Jobs;
using MegaBuy.MegaBuyCorporation;
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

        private IVisualControl _roleUI = RoleTraits.Controls[JobRole.ReferrerLevel1];
        private Call _call;
        private bool _isInCall = false;
        private bool _isCalling = false;

        public ClickUIBranch Branch { get; private set; }
        public App Type => App.Call;

        public CallApp()
        {
            Branch = new ClickUIBranch("Call App", (int)ClickUIPriorities.Pad);
            Branch.Add(_ready.Branch);
            World.Subscribe(EventSubscription.Create<AgentCallStatusChanged>(x => StartConnecting(x), this));
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
            World.Subscribe(EventSubscription.Create<JobRoleAccepted>(Promote, this));
        }

        public void Update(TimeSpan delta)
        {
            _messenger.Update(delta);
            if (_isInCall)
                _call.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _messenger.Draw(absoluteTransform);
            _caller.Draw(absoluteTransform);
            _callOptions.Draw(absoluteTransform);
            _roleUI.Draw(absoluteTransform);
            if (!_isCalling)
            {
                _ready.Draw(absoluteTransform);
                _rating.Draw(absoluteTransform);
            }
        }

        private void StartConnecting(AgentCallStatusChanged status)
        {
            if (status.Status != AgentCallStatus.Available)
                return;
            _isCalling = true;
            Branch.Remove(_ready.Branch);
        }

        private void StartCall(Call call)
        {
            _call = call;
            _isInCall = true;
            Branch.Add(_callOptions.Branch);
            Branch.Add(_roleUI.Branch);
        }

        private void EndCall()
        {
            _isCalling = false;
            _isInCall = false;
            Branch.Remove(_callOptions.Branch);
            Branch.Remove(_roleUI.Branch);
            Branch.Add(_ready.Branch);
        }

        private void Promote(JobRoleAccepted promotion)
        {
            // @ todo #1 Make promotions call safe
            //currently with this new code i wrote it will cause a crash if one were to be promoted during a call
            //this could be done by not allowing promotions during calls, store the promotion until end of call, or end the call they are currently in 
            _roleUI = RoleTraits.Controls[promotion.JobRole];
        }
    }
}
