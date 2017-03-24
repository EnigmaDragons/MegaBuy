﻿using System;
using MegaBuy.UIStuff;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class Screen : IScene
    {
        private ChatBox _box;

        public void Init()
        {
            _box = new ChatBox("sdf sjeifgj srjgjngj njnrgnjasrwntnjwer nolawnsogfns ggjisfgnjgn n rgjsg ni iswgf sgnewnrgn gjn jna gjnae ognaeg nagna ogjnejnarjgne ngn n rfgn jusgnsgju hnsghnas gnasgnsdajnhakjba swugs g lsnjdfj sgfbas fkab wfbna fanfabgfkjb sghbhsbcvhbxcvbhewuir reswoghfi irgfjdg ihnsfg oihnjsrgfswre jihnsfsw t ihnjsdfsf isrgf ijswdrgfe joirsgj aisjg swdfg sddgfijsogfij sdgfkjm sdfkjksgfj sdkfjksdfe njsdfkljnsd ewrhnwoerhn sdkflnj eefrkijhnkisf kjfs kisnjoklsnagv klgfklsdnjgfl fksdjfnjsf sdklfjklsf sdklfjklj sfklj kljsdfk jkl", 1500, DefaultFont.Font);
        }

        public void Update(TimeSpan delta)
        {
            _box.Update(delta);
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Rectangle(0, 0, 1600, 900));
            UI.DrawCenteredWithOffset("Images/Screen/male-customer", new Vector2(0, -20));

            World.Draw("Images/Screen/conversation", new Vector2(0, 700));
            //World.Draw("Images/Screen/button", new Vector2(100, 750));
            //World.Draw("Images/Screen/button", new Vector2(900, 750));
            _box.Draw(new Transform2(new Vector2(50, 725), new Size2(1500, 150)));
        }


    }
}
