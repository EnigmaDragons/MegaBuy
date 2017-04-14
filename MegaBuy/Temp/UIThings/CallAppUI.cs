using System;
using MegaBuy.Apps;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Temp.UIThings
{
    public class CallAppUI : IApp
    {
        private Transform2 _transform = Transform2.Zero;
        private readonly CallMessengerUI _messenger = new CallMessengerUI();

        public App Type => App.Call;

        public void Update(TimeSpan delta)
        {
            _messenger.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _messenger.Draw(parentTransform + _transform);
        }

        public void LostFocus()
        {
        }

        public void GainedFocus()
        {
        }
    }
}
