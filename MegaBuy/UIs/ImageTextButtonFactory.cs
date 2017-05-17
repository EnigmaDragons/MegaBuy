﻿using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.UIs
{
    public class ImageTextButtonFactory
    {
        public static ImageTextButton Create(string text, Vector2 location, Action onClick)
        {
            return Create(text, location, onClick, () => true);
        }

        public static ImageTextButton Create(string text, Vector2 location, Action onClick, Func<bool> isVisible)
        {
            return new ImageTextButton(text,
                "Images/UI/button-press", "Images/UI/button", "Images/UI/button-hover",
                new Transform2(location, Sizes.Button), onClick, isVisible);
        }

        public static ImageTextButton CreateTrapazoid(string text, Vector2 location, Action onClick)
        {
            return new ImageTextButton(text,
                "Images/UI/button-trapazoid", "Images/UI/button-trapazoid-press", "Images/UI/button-trapazoid-hover", 
                new Transform2(location, Sizes.PadToggle), onClick);
        }

        public static ImageTextButton CreateRotated(string text, Vector2 location, Action onClick)
        {
            return CreateRotated(text, location, onClick, () => true);
        }

        public static ImageTextButton CreateRotated(string text, Vector2 location, Action onClick, Func<bool> isVisible)
        {
            return new ImageTextButton(text,
                "Images/UI/button-side-press", "Images/UI/button-side", "Images/UI/button-side-hover",
                new Transform2(location, Sizes.SideButton), onClick, isVisible);
        }
    }
}
