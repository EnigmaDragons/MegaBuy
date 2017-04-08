using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public class SingleImageButton : ClickableUIElement, IVisual
    {
        private readonly string _image;
        
        private readonly ColoredRectangle _default;
        private readonly ColoredRectangle _hover;
        private readonly ColoredRectangle _press;

        private readonly Transform2 _transform;
        private readonly Action _onClick;

        private ColoredRectangle _current;

        public SingleImageButton(string image, Color hover, Color press, Transform2 transform, Action onClick) : base(10, transform.ToRectangle())
        {
            _image = image;
            _default = new ColoredRectangle();
            _default.Color = Color.Transparent;
            _default.Transform = transform;
            _hover = new ColoredRectangle();
            _hover.Color = hover;
            _hover.Transform = transform;
            _press = new ColoredRectangle();
            _press.Color = hover;
            _press.Transform = transform;
            _transform = transform;
            _onClick = onClick;
            _current = _default;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_image, parentTransform + _transform);
            _current.Draw(parentTransform);
        }

        public override void OnEntered()
        {
            _current = _hover;
        }

        public override void OnExitted()
        {
            _current = _default;
        }

        public override void OnPressed()
        {
            _current = _press;
        }

        public override void OnReleased()
        {
            _current = _default;
            _onClick.Invoke();
        }

        public void Dispose()
        {
            _default.Dispose();
            _hover.Dispose();
            _press.Dispose();
            _current.Dispose();
        }
    }
}
