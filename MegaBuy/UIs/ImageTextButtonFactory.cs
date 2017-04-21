using System;
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

        public static ImageTextButton CreateTrapazoid(string text, Vector2 location, Action onClick)
        {
            return new ImageTextButton(text,
                "Images/UI/button-trapazoid", "Images/UI/button-trapazoid-hover", "Images/UI/button-trapazoid-press",
                new Transform2(location, Sizes.PadToggle), onClick);
        }
    }
}
