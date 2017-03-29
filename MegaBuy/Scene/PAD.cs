using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;

namespace MegaBuy.Scene
{
    class PAD : IScene
    {
        private ImageButton2 _btn;

        public void Init()
        {
            _btn = new ImageButton2("", "", "", new Transform2(new Vector2(600, 500), new Size2(200, 50)), () => World.NavigateToScene("Room"));
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
