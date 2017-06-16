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
        private CharacterSex _sex = CharacterSex.Male;

        private readonly KeyboardTyping _typing = new KeyboardTyping();
        private ClickUI _ui;
        private Label _createCharacterLabel;
        private Label _charName;
        private Label _charSexLabel;
        private ImageTextButton _startGameButton;
        private ImageTextButton _maleButton;
        private ImageTextButton _femaleButton;

        public void Init()
        {
            _createCharacterLabel = new Label {BackgroundColor = Color.Transparent, TextColor = Color.White, Text = "Create Your Character", Transform = new Transform2(Sizes.LargeLabel) };
            _charName = new Label { BackgroundColor = Color.Black, TextColor = Color.White, Text = "Name", Transform = new Transform2(Sizes.MediumLabel) };
            _charSexLabel = new Label { BackgroundColor = Color.Black, TextColor = Color.White, Text = "Make", Transform = new Transform2(Sizes.MediumLabel) };
            _startGameButton = new ImageTextButton("Start Game",
                "Images/Menu/button-default",
                "Images/Menu/button-hover",
                "Images/Menu/button-pressed",
                new Transform2(new Vector2(800 - 100, 750 - 22),
                new Size2(200, 44)), StartGame);
            _maleButton = new ImageTextButton("Male",
                "Images/Menu/button-default",
                "Images/Menu/button-hover",
                "Images/Menu/button-pressed",
                new Transform2(new Vector2(800 - 100, 650 - 22),
                new Size2(200, 44)), () => _sex = CharacterSex.Male);
            _femaleButton = new ImageTextButton("Female",
                 "Images/Menu/button-default",
                 "Images/Menu/button-hover",
                 "Images/Menu/button-pressed",
                 new Transform2(new Vector2(800 - 100, 600 - 22),
                 new Size2(200, 44)), () => _sex = CharacterSex.Female);
            _ui = new ClickUI();
            _ui.Add(_startGameButton);
            _ui.Add(_maleButton);
            _ui.Add(_femaleButton);
        }

        private void StartGame()
        {
            CurrentGameState.SetupCharacter(_charName.RawText, _sex);
            World.NavigateToScene("InGame");
        }

        public void Update(TimeSpan delta)
        {
            _typing.Update(delta);
            _charName.Text = _typing.Result.Length > 0 ? _typing.Result : "Name";
            _charSexLabel.Text = _sex.ToString();
            _ui.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Menu/menu", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            UI.DrawCenteredWithOffset("Images/Screen/mega-buy", new Vector2(0, -300));
            _createCharacterLabel.Draw(new Transform2(new Vector2(500, 200)));
            _startGameButton.Draw(Transform2.Zero);
            _charName.Draw(new Transform2(new Vector2(700, 400)));
            _charSexLabel.Draw(new Transform2(new Vector2(700, 475)));
            _maleButton.Draw(Transform2.Zero);
            _femaleButton.Draw(Transform2.Zero);
        }
    }
}