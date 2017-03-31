using System;
using MegaBuy.Calls;
using MegaBuy.Calls.Rules;
using MegaBuy.Calls.UIThings;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;

namespace MegaBuy.Scene
{
    public sealed class PAD : IScene
    {
        private ImageButton _leave;
        private IconButton _food;
        private IconButton _calls;
        private ClickUI _ui;

        private CallApp _app;

        public void Init()
        {
            _leave = new ImageButton(
                "Images/Icons/poweroff",
                "Images/Icons/poweroff-hover",
                "Images/Icons/poweroff-hover",
                new Transform2(new Vector2(52, 750), new Size2(90, 90)),
                () => World.NavigateToScene("Room"));

            var iconRect = new Rectangle(new Point(55, 10), new Point(90, 90));

            _food = new IconButton("Images/Icons/burger", iconRect, new Rectangle(new Point(0, 0), new Point(200, 110)),
                Color.LightBlue, Color.Blue, Color.DarkBlue);

            _calls = new IconButton("Images/Icons/video-call", iconRect, new Rectangle(new Point(0, 110), new Point(200, 110)),
                Color.LightBlue, Color.Blue, Color.DarkBlue);

            _ui = new ClickUI();
            _ui.Add(_leave);
            _ui.Add(_food);
            _ui.Add(_calls);

            _app = new CallApp(new CallGenerater(CallCenterPosition.Referrer).GenerateCall());
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
            _app.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            _food.Draw(Transform2.Zero);
            _calls.Draw(Transform2.Zero);
            _leave.Draw(Transform2.Zero);
            _app.Draw(new Transform2(new Vector2(200, 0), new Size2(0, 0)));
        }
    }
}
