using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Endings
{
    public sealed class HeartAttack : IScene
    {
        public void Init()
        {
        }

        public void Update(TimeSpan delta)
        {
        }

        // @todo #1 Design Heart Attack Game Over Scene appearance
        public void Draw()
        {
            UI.DrawText("GAME OVER: You died of a heart attack!", Vector2.Zero, Color.White);
        }
    }
}
