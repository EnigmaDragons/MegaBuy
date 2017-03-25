using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Endings
{
    public sealed class Starved : IScene
    {
        public void Init()
        {
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            UI.DrawText("You Starved To Death", Vector2.Zero, Color.White);
        }
    }
}
