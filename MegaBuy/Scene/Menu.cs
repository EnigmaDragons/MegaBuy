using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Audio;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class Menu : IScene
    {
        private ImageTextButton _btn;
        private ClickUI _ui;

        public void Init()
        { 
            _btn = new ImageTextButton("New Game",
                "Images/Menu/button-default",
                "Images/Menu/button-hover",
                "Images/Menu/button-pressed",
                new Transform2(new Vector2(800 - 100, 750 - 22),
                new Size2(200, 44)), () => World.NavigateToScene("CharacterCreation"));

            _ui = new ClickUI();
            _ui.Add(_btn);
            Audio.PlayMusic("Music/maintheme");
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
        }
    }
}
