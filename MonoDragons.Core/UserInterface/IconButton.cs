using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class IconButton : ClickableUIElement, IVisual
    {
        private readonly string _icon;
        private readonly Color _defaultColor;
        private readonly Color _hover;
        private readonly Color _pressed;
        private readonly Action _onPressed;
        private readonly Rectangle _buttonArea;
        private readonly Rectangle _iconArea;

        private Color _color;

        public IconButton(string icon, Rectangle iconArea, Rectangle buttonArea, Color defaultColor, Color hover, Color pressed, Action onPressed) 
            : base(10, buttonArea)
        {
            _icon = icon;
            _iconArea = iconArea;
            _iconArea.Offset(buttonArea.Location);
            _defaultColor = defaultColor;
            _hover = hover;
            _pressed = pressed;
            _onPressed = onPressed;
            _color = _defaultColor;
            _buttonArea = buttonArea;
        }

        public override void OnEntered()
        {
            _color = _hover;
        }

        public override void OnExitted()
        {
            _color = _defaultColor;
        }

        public override void OnPressed()
        {
            _color = _pressed;
        }

        public override void OnReleased()
        {
            _color = _hover;
            _onPressed();
        }

        public void Draw(Transform2 parentTransform)
        {
            var buttonArea = new Rectangle(_buttonArea.Location, _buttonArea.Size);
            buttonArea.Offset(parentTransform.Location);
            World.DrawRectangle(_buttonArea, _color);
            var iconArea = new Rectangle(_iconArea.Location, _iconArea.Size);
            iconArea.Offset(parentTransform.Location);
            World.Draw(_icon, iconArea);
        }
    }
}
