using System;
using MegaBuy.Calls;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Events;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.ReturnCalls.Callers
{
    public class CallerFaceUI : ISpatialVisual, IAutomaton
    {
        private string _caller;
        private bool _isInCall = false;
        private float _scale = 1f;
        private bool _hangingUp = false;

        public Transform2 Transform { get; }

        public CallerFaceUI()
        {
            Transform = new Transform2(new Size2(250, 410));
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
            World.Subscribe(EventSubscription.Create<CallerHangupStarted>(x => OnCallerHangup(), this));
        }

        public void Update(TimeSpan delta)
        {
            if (_scale == 0f)
                World.Publish(new CallerHangupFinished());
            if (_hangingUp)
                _scale = Math.Max(_scale - (float)(delta.TotalMilliseconds / 2000), 0f);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isInCall)
                return;
            World.Draw("Images/UI/caller", parentTransform + Transform);
            var unscaledTransform = Transform + new Transform2(new Size2(-10, -10));
            var scaledSize = new Size2((int)(unscaledTransform.Size.Width * _scale), (int) (unscaledTransform.Size.Height * _scale));
            var scaledTransform = new Transform2(new Vector2((250 - scaledSize.Width) / 2, (410 - scaledSize.Height) / 2), scaledSize);
            World.Draw("Images/Customers/" + _caller, parentTransform + scaledTransform);
        }

        private void StartCall(Call call)
        {
            _hangingUp = false;
            _isInCall = true;
            _caller = call.Caller.Name.ToLower().Replace(" ", "-");
            _scale = 1f;
        }

        private void EndCall()
        {
            _isInCall = false;
        }

        private void OnCallerHangup()
        {
            _hangingUp = true;
        }
    }
}
