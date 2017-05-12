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
                "Images/UI/button-press", "Images/UI/button", "Images/UI/button-hover",
                new Transform2(location, Sizes.Button), onClick);
        }

        public static ImageTextButton CreateTrapazoid(string text, Vector2 location, Action onClick)
        {
            return new ImageTextButton(text,
                "Images/UI/button-trapazoid", "Images/UI/button-trapazoid-press", "Images/UI/button-trapazoid-hover", 
                new Transform2(location, Sizes.PadToggle), onClick);
        }

        public static ImageTextButton CreateRotated(string text, Vector2 location, Action onClick, Rotation2 rotation)
        {
            return new ImageTextButton(text,
                "Images/UI/button-press", "Images/UI/button", "Images/UI/button-hover",
                new Transform2(location, rotation, Sizes.Button, 1), onClick);
        }
    }
}
