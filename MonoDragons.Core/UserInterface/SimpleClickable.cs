﻿using System;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.UserInterface
{
    public sealed class SimpleClickable : ClickableUIElement
    {
        private readonly Action _onClick;

        public SimpleClickable(int layer, Rectangle area, Action onClick) : base(layer, area)
        {
            _onClick = onClick;
        }

        public override void OnEntered()
        {
        }

        public override void OnExitted()
        {
        }

        public override void OnPressed()
        {
        }

        public override void OnReleased()
        {
            _onClick();
        }
    }
}
