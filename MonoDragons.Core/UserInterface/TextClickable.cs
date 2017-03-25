﻿using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class TextClickable : ClickableUIElement, IVisual
    {
        private readonly string _text;
        private readonly Rectangle _area;
        private readonly Action _onClick;

        public TextClickable(string text, int layer, Rectangle area, Action action) : base(layer, area)
        {
            _area = area;
            _text = text;
        }

        public void Draw(Transform2 parentTransform)
        {
            UI.DrawText(_text, _area.Location.ToVector2(), Color.Yellow);
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
