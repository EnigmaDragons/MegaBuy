using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.Engine
{
    public static class GameInstance
    {
        public static Game TheGame { get; private set; }
        public static GraphicsDevice GraphicsDevice => TheGame.GraphicsDevice;

        public static void Init(Game game)
        {
            TheGame = game;
        }
    }
}
