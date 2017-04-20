using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Endings
{
    public sealed class Evicted : IScene
    {
        public void Init()
        {
        }

        public void Update(TimeSpan delta)
        {
        }
        
        public void Draw()
        {
            UI.DrawCentered("Images/Endings/evicted", new Vector2(1600, 900));
            UI.DrawCenteredWithOffset("Images/Endings/gameover", new Vector2(0, -100));
            UI.DrawTextCentered("You've been evicted", new Rectangle(0, 100, 1600, 800), Color.LightGray);
        }
    }
}
