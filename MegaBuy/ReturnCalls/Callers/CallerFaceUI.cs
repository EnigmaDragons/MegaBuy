using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.ReturnCalls.Callers
{
    public class CallerFaceUI : ISpatialVisual
    {
        private string _caller;
        private bool _isInCall = false;

        public Transform2 Transform { get; }

        public CallerFaceUI()
        {
            Transform = new Transform2(new Size2(250, 410));
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/caller", parentTransform + Transform);
            if (_isInCall)
                World.Draw("Images/Customers/" + _caller, parentTransform + Transform + new Transform2(new Vector2(5, 5), new Size2(-10, -10)));
        }

        private void StartCall(Call call)
        {
            _isInCall = true;
            _caller = call.Caller.Name.ToLower().Replace(" ", "-");
        }

        private void EndCall()
        {
            _isInCall = false;
        }
    }
}
