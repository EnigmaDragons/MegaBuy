using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public class TextButton : ClickableUIElement, IVisual
    {
        private readonly Action _onClick;
        private readonly string _text;
        private readonly Color _defaultColor;
        private readonly Color _hover;
        private readonly Color _press;
        private Color _currentColor;

        public TextButton(int layer, Rectangle area, Action onClick, string text, Color defaultColor, Color hover, Color press) : base(layer, area)
        {
            _onClick = onClick;
            _text = text;
            _defaultColor = defaultColor;
            _hover = hover;
            _press = press;
            _currentColor = _defaultColor;
        }

        public override void OnEntered()
        {
            _currentColor = _hover;
        }

        public override void OnExitted()
        {
            _currentColor = _defaultColor;
        }

        public override void OnPressed()
        {
            _currentColor = _press;
        }

        public override void OnReleased()
        {
            _currentColor = _defaultColor;
            _onClick();
        }

        public void Draw(Transform2 parentTransform)
        {
            var str = DefaultFont.Font.MeasureString(_text);
            var x = parentTransform.Location.X + Area.X + ((Area.Width - str.X)/2);
            var y = parentTransform.Location.Y + Area.Y + ((Area.Height - str.Y) / 2);
            World.Draw(new RectangleTexture(Area.Width, Area.Height, _currentColor).Create(), new Vector2(Area.X + parentTransform.Location.X, Area.Y + parentTransform.Location.Y));
            UI.DrawText(_text, new Vector2(x, y), Color.White);
        }
    }
}
