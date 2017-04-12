using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Endings
{
    public class Fired : IScene
    {
        public void Init()
        {
        }

        public void Update(TimeSpan delta)
        {
        }

        // @todo #1 Design Fired Game Over Scene appearance
        public void Draw()
        {
            UI.DrawText("GAME OVER: You've been fired!", Vector2.Zero, Color.White);
        }
    }
}
