using System;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class CharacterCreation : IScene
    {
        private Label _createCharacterLabel;
        private ImageTextButton _startGameButton;
        private ClickUI _ui;

        public void Init()
        {
            _createCharacterLabel = new Label {BackgroundColor = Color.Transparent, TextColor = Color.White, Text = "Create Your Character", Transform = new Transform2(Sizes.LargeLabel) };
            _startGameButton = new ImageTextButton("Start Game",
                "Images/Menu/button-default",
                "Images/Menu/button-hover",
                "Images/Menu/button-pressed",
                new Transform2(new Vector2(800 - 100, 750 - 22),
                new Size2(200, 44)), () => World.NavigateToScene("InGame"));

            _ui = new ClickUI();
            _ui.Add(_startGameButton);
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Menu/menu", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            UI.DrawCenteredWithOffset("Images/Screen/mega-buy", new Vector2(0, -300));
            _createCharacterLabel.Draw(new Transform2(new Vector2(500, 200)));
            _startGameButton.Draw(new Transform2(new Vector2(0, 0)));
        }
    }
}