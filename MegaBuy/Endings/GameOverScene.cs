using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Endings
{
    public abstract class GameOverScene : IScene
    {
        private readonly string _image;
        private readonly string _gameOverReason;
        private readonly ClickUI _clickUI;

        protected GameOverScene(string image, string gameOverReason)
        {
            _image = image;
            _gameOverReason = gameOverReason;
            _clickUI = new ClickUI();
            _clickUI.Add(new ScreenClickable(() => { World.NavigateToScene("MainMenu"); }));
        }

        public void Init()
        {
        }

        public void Update(TimeSpan delta)
        {
            _clickUI.Update(delta);
        }

        public void Draw()
        {
            UI.DrawCentered("Images/Endings/" + _image, new Vector2(1600, 900));
            UI.DrawCenteredWithOffset("Images/Endings/gameover", new Vector2(0, -100));
            UI.DrawTextCentered(_gameOverReason, new Rectangle(0, 100, 1600, 800), Color.LightGray);
        }
    }
}
