using System;
using MegaBuy.Calls;
using MegaBuy.Calls.Rules;
using MegaBuy.Calls.UIThings;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;

//1400 width
//900 height

namespace MegaBuy.Scene
{
    public sealed class PAD : IScene
    {
        private ImageButton _leave;
        private ClickUI _ui;

        private CallApp _app;

        public void Init()
        {
            _leave = new ImageButton(
                "Images/PAD/logout-default",
                "Images/PAD/logout-hover",
                "Images/PAD/logout-pressed",
                new Transform2(new Vector2(0, 0), new Size2(200, 50)),
                () => World.NavigateToScene("Room"));

            _ui = new ClickUI();
            _ui.Add(_leave);

            _app = new CallApp(_ui, new CallGenerater(CallCenterPosition.Referrer).GenerateCall());
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
            _app.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            _leave.Draw(Transform2.Zero);
            _app.Draw(new Transform2(new Vector2(200, 0), new Size2(0, 0)));
        }
    }
}
