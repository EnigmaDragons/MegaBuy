using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class Screen : IScene
    {
        public void Init()
        {
            
        }

        public void Update(TimeSpan delta)
        {
            
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen", new Rectangle(0, 0, 1600, 900));
            UI.DrawCentered("Images/Screen/mega-buy");
        }
    }
}
