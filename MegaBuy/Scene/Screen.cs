using System;
using MegaBuy.UIStuff;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class Screen : IScene
    {
        private ClickUI _clickUi; 
        private ChatBox _box;

        public void Init()
        {
            _box = new ChatBox("My flying car just arrived, but I can't get it to turn on!", 1500, DefaultFont.Font);
            _clickUi = new ClickUI();
            _clickUi.Add(new SimpleClickable(0, new Rectangle(0, 0, 1920, 1080), () => _box.CompletelyDisplayMessage()));
        }

        public void Update(TimeSpan delta)
        {
            _box.Update(delta);
            _clickUi.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Rectangle(0, 0, 1600, 900));
            UI.DrawCenteredWithOffset("Images/Screen/male-customer", new Vector2(0, -20));

            World.Draw("Images/Screen/conversation", new Vector2(0, 700));
            //World.Draw("Images/Screen/button", new Vector2(100, 750));
            //World.Draw("Images/Screen/button", new Vector2(900, 750));
            _box.Draw(new Transform2(new Vector2(50, 725), new Size2(1500, 150)));
        }


    }
}
