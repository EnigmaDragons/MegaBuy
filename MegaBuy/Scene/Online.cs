using System;
using MegaBuy.Apps;
using MegaBuy.CustomUI;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scene
{
    public sealed class Online : IScene
    {
        private Overlay _overlay;
        private PAD _pad;

        public void Init()
        {
            _overlay = new Overlay();
            _pad = GameState.PAD;
            _pad.OpenApp(App.Call);
            Input.On(Control.X, () => World.NavigateToScene("Room"));
        }

        public void Update(TimeSpan delta)
        {
            _pad.Update(delta);
            _overlay.Update(delta);
        }

        public void Draw()
        {
            _pad.Draw(Transform2.Zero);
            _overlay.Draw(Transform2.Zero);
        }
    }
}
