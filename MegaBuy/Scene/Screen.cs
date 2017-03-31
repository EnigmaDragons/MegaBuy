using System;
using MegaBuy.Calls.UIThings;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class Screen : IScene
    {
        private CallApp _app;
        private ClickUI _clickUi; 

        public void Init()
        {
            _clickUi = new ClickUI();
            //_clickUi.Add(new SimpleClickable(0, new Rectangle(0, 0, 1920, 1080), () => _box.CompletelyDisplayMessage()));
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Rectangle(0, 0, 1600, 900));
            _app.Draw(new Transform2(new Vector2(500, 100), new Size2(600, 600)));
        }
    }
}
