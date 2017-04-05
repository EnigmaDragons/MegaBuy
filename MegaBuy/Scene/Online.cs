using System;
using MegaBuy.Apps;
using MegaBuy.Calls;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scene
{
    public sealed class Online : IScene
    {
        private CallQueue _callQueue;
        private PAD _pad;

        public void Init()
        {
            _pad = new PAD();
            _pad.OpenApp(App.Call);
            _callQueue = new CallQueue();
        }

        public void Update(TimeSpan delta)
        {
            _pad.Update(delta);
        }

        public void Draw()
        {
            _pad.Draw(Transform2.Zero);
        }
    }
}
