using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public sealed class ClickToWin : IScene
    {
        private ImageButton2 _button;
        private ClickUI _ui;

        public void Init()
        {
            _button = new ImageButton2(
                "Images/Screen/button-default", 
                "Images/Screen/button-hover", 
                "Images/Screen/button-pressed", 
                new Transform2(new Vector2(150, 150), new Size2(450, 100)), 
                () => Environment.Exit(0));

            _ui = new ClickUI();
            _ui.Add(_button);
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
        }

        public void Draw()
        {
            World.DrawBackgroundColor(Color.White);
            _button.Draw(Transform2.Zero);
        }
    }
}
