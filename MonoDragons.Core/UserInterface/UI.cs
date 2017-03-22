using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Memory;
using MonoDragons.Core.UI;

namespace MonoDragons.Core.UserInterface
{
    public static class UI
    {
        private static Game _game;
        private static SpriteBatch _spriteBatch;

        public static void Init(Game game, SpriteBatch spriteBatch)
        {
            _game = game;
            _spriteBatch = spriteBatch;
        }

        public static void DrawBackgroundColor(Color color)
        {
            _game.GraphicsDevice.Clear(color);
        }

        public static void DrawCentered(string imageName)
        {
            DrawCenteredWithOffset(imageName, Vector2.Zero);
        }

        public static void DrawCentered(string imageName, Vector2 widthHeight)
        {
            DrawCenteredWithOffset(imageName, widthHeight, Vector2.Zero);
        }

        public static void DrawCenteredWithOffset(string imageName, Vector2 offSet)
        {
            var texture = Resources.Load<Texture2D>(imageName);
            DrawCenteredWithOffset(imageName, new Vector2(texture.Width, texture.Height), offSet);
        }

        public static void DrawCenteredWithOffset(string imageName, Vector2 widthHeight, Vector2 offSet)
        {
            _spriteBatch.Draw(Resources.Load<Texture2D>(imageName), null,
                new Rectangle(ScalePoint(_game.GraphicsDevice.Viewport.Width / 2 / 1 - widthHeight.X / 2 + offSet.X,
                _game.GraphicsDevice.Viewport.Height / 2 / 1 - widthHeight.Y / 2 + offSet.Y), ScalePoint(widthHeight.X, widthHeight.Y)),
                null, null, 0, new Vector2(1, 1));
        }

        public static void DrawText(string text, Vector2 position, Color color)
        {
            _spriteBatch.DrawString(DefaultFont.Font, text, position, color);
        }

        private static Point ScalePoint(float x, float y)
        {
            return new Point((int)Math.Round(x * 1), (int)Math.Round(y * 1));
        }
    }
}
