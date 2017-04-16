using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.UIs
{
    public class ImageTextButtonFactory
    {
        public static ImageTextButton Create(string text, Vector2 location, Action onClick)
        {
            return new ImageTextButton(text, 
                "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(location, Sizes.Button), onClick);
        }
    }
}
