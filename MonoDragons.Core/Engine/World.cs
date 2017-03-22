using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.UI;

namespace MonoDragons.Core.Engine
{
    public static class World
    {
        private static readonly Events _events = new Events();

        private static Game _game;
        private static ContentManager _content;
        private static SpriteBatch _spriteBatch;
        private static INavigation _navigation;

        private static float _scale = 1;

        public static void Init(Game game, INavigation navigation, SpriteBatch spriteBatch)
        {
            _game = game;
            _content = game.Content;
            _navigation = navigation;
            _spriteBatch = spriteBatch;
            DefaultFont.Load(_content);
        }

        public static void Draw(Texture2D texture, Rectangle rectangle, Color color)
        {
            _spriteBatch.Draw(texture, rectangle, color);
        }

        public static void PlaySound(string soundName)
        {
            Resources.Load<SoundEffect>(soundName).Play();
        }

        public static void PlayMusic(string songName)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(Resources.Load<Song>(songName));
        }

        public static void NavigateToScene(string sceneName)
        {
            Resources.Unload();
            _navigation.NavigateTo(sceneName);
        }

        public static void DrawBackgroundColor(Color color)
        {
            _game.GraphicsDevice.Clear(color);
        }

        public static void Draw(string imageName, Vector2 pixelPosition)
        {
            _spriteBatch.Draw(Resources.Load<Texture2D>(imageName), pixelPosition);
        }

        public static void Draw(string imageName, Rectangle rectPostion)
        {
            _spriteBatch.Draw(Resources.Load<Texture2D>(imageName), rectPostion, Color.White);
        }

        public static void DrawCircle(float radius, Color color, Vector2 position)
        {
            var circle = new CircleTexture((int) radius, color).Create();
            _spriteBatch.Draw(circle, position);
        }

        public static void Publish<T>(T payload)
        {
            _events.Publish(payload);
        }

        public static void Subscribe<T>(EventSubscription<T> subscription)
        {
            _events.Subscribe(subscription);
            Resources.Put(Guid.NewGuid().ToString(), subscription);
        }

        public static void Unsubscribe(object owner)
        {
            _events.Unsubscribe(owner);
        }

        public static void DrawRectangle(Rectangle rectangle, Color color)
        {
            var rect = new RectangleTexture(rectangle.Width, rectangle.Height, color).Create();
            Resources.Put(Guid.NewGuid().ToString(), rect);
            _spriteBatch.Draw(rect, rectangle, color);
        }

        public static void Draw(Texture2D texture, Vector2 position)
        {
            Resources.Put(texture.GetHashCode().ToString(), texture);
            _spriteBatch.Draw(texture, position);
        }
    }
}
