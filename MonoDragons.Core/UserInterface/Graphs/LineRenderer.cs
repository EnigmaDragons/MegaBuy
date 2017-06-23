using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.UserInterface.Graphs
{
    public static class LineRenderer
    {
        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end)
        {
            spriteBatch.Draw(texture, start, null, Color.White,
                (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                new Vector2(0f, (float)texture.Height / 2),
                new Vector2(Vector2.Distance(start, end), 1f),
                SpriteEffects.None, 0f);
        }
    }
}
