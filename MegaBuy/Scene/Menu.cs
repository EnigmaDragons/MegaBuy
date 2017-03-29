using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class Menu : IScene
    {
        private ImageButton _btn;
        private ClickUI _ui;

        public void Init()
        { 
            _btn = new ImageButton(
                "Images/Menu/button-default",
                "Images/Menu/button-hover",
                "Images/Menu/button-pressed",
                new Transform2(new Vector2(800 - 100, 750 - 22),
                new Size2(200, 44)), () => World.NavigateToScene("Room"));

            _ui = new ClickUI();
            _ui.Add(_btn);
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Menu/menu", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            UI.DrawCenteredWithOffset("Images/Screen/mega-buy", new Vector2(0, -300));
            _btn.Draw(new Transform2(new Vector2(0, 0)));
            UI.DrawText("Start", new Vector2(772, 735), Color.White);
        }
    }
}
