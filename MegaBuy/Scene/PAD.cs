using System;
using MegaBuy.Calls.UIThings;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;

namespace MegaBuy.Scene
{
    public sealed class PAD : IScene
    {
        private ImageButton _callApp;
        private ImageButton _foodApp;
        private ImageButton _leave;
        private ClickUI _ui;

        private CallApp _app;

        public void Init()
        {
            _callApp = new ImageButton(
                "Images/PAD/available-default",
                "Images/PAD/available-hover",
                "Images/PAD/available-pressed",
                new Transform2(new Vector2(200, 500), new Size2(200, 50)),
                () => { });

            _foodApp = new ImageButton(
                "Images/PAD/orderfood-default",
                "Images/PAD/orderfood-hover",
                "Images/PAD/orderfood-pressed",
                new Transform2(new Vector2(200, 600), new Size2(200, 50)),
                () => { });

            _leave = new ImageButton(
                "Images/PAD/logout-default",
                "Images/PAD/logout-hover",
                "Images/PAD/logout-pressed",
                new Transform2(new Vector2(200, 700), new Size2(200, 50)),
                () => World.NavigateToScene("Room"));

            _ui = new ClickUI();
            _ui.Add(_callApp);
            _ui.Add(_foodApp);
            _ui.Add(_leave);

            _app = new CallApp(_ui);
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
            _app.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            _callApp.Draw(Transform2.Zero);
            _foodApp.Draw(Transform2.Zero);
            _leave.Draw(Transform2.Zero);
            _app.Draw(new Transform2(new Vector2(200, 0), new Size2(0, 0)));
        }
    }
}
