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
            World.Draw("Images/Screen/screen2", new Rectangle(0, 0, 1600, 900));
            UI.DrawCenteredWithOffset("Images/Screen/male-customer", new Vector2(0, -20));
            //World.Draw("Images/Screen/conversation", new Vector2(0, 700));
            World.Draw("Images/Screen/button", new Vector2(100, 50));
            World.Draw("Images/Screen/button", new Vector2(900, 50));
            World.Draw("Images/Screen/button", new Vector2(100, 200));
            World.Draw("Images/Screen/button", new Vector2(900, 200));
            World.Draw("Images/Screen/button", new Vector2(100, 350));
            World.Draw("Images/Screen/button", new Vector2(900, 350));
            World.Draw("Images/Screen/button", new Vector2(100, 500));
            World.Draw("Images/Screen/button", new Vector2(900, 500));
            World.Draw("Images/Screen/button", new Vector2(100, 650));
            World.Draw("Images/Screen/button", new Vector2(900, 650));
        }
    }
}
