using System;
using System.Linq;
using MegaBuy.Player;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class CharacterCreation : IScene
    {
        private readonly KeyboardTyping _typing = new KeyboardTyping();
        private Label _createCharacterLabel;
        private Label _charName;
        private ImageTextButton _startGameButton;
        private ClickUI _ui;

        public void Init()
        {
            _createCharacterLabel = new Label {BackgroundColor = Color.Transparent, TextColor = Color.White, Text = "Create Your Character", Transform = new Transform2(Sizes.LargeLabel) };
            _charName = new Label { BackgroundColor = Color.Black, TextColor = Color.White, Text = "Name", Transform = new Transform2(Sizes.MediumLabel)};
            _startGameButton = new ImageTextButton("Start Game",
                "Images/Menu/button-default",
                "Images/Menu/button-hover",
                "Images/Menu/button-pressed",
                new Transform2(new Vector2(800 - 100, 750 - 22),
                new Size2(200, 44)), StartGame);

            _ui = new ClickUI();
            _ui.Add(_startGameButton);
        }

        private void StartGame()
        {
            CurrentGameState.SetupCharacter(_charName.RawText, CharacterSex.Male);
            World.NavigateToScene("InGame");
        }

        public void Update(TimeSpan delta)
        {
            _typing.Update(delta);
            _charName.Text = _typing.Result.Length > 0 ? _typing.Result : "Name";
            _ui.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Menu/menu", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            UI.DrawCenteredWithOffset("Images/Screen/mega-buy", new Vector2(0, -300));
            _createCharacterLabel.Draw(new Transform2(new Vector2(500, 200)));
            _startGameButton.Draw(new Transform2(new Vector2(0, 0)));
            _charName.Draw(new Transform2(new Vector2(600, 400)));
        }
    }
}