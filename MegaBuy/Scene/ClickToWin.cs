﻿using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public sealed class ClickToWin : IScene
    {
        private ImageButton _button;

        public void Init()
        {
            _button = new ImageButton(
                "Images/Screen/button-default", 
                "Images/Screen/button-hover", 
                "Images/Screen/button-pressed", 
                new Transform2(new Vector2(150, 150), new Size2(450, 100)), 
                () => Environment.Exit(0));
        }

        public void Update(TimeSpan delta)
        {
            _button.Update(delta);
        }

        public void Draw()
        {
            World.DrawBackgroundColor(Color.White);
            _button.Draw(Transform2.Zero);
        }
    }
}