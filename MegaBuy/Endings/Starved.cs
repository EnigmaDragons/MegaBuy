﻿using System;
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
        // @todo #1 Design Starved Game Over Scene appearance
        public void Draw()
        {
            UI.DrawText("You Starved To Death", Vector2.Zero, Color.White);
        }
    }
}
