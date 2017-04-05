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
        private readonly Texture2D _default;
        private readonly Texture2D _hover;
        private readonly Texture2D _press;

        private readonly Transform2 _transform;
        private readonly Action _onClick;

        private Texture2D _current;

        public SingleImageButton(string image, Color hover, Color press, Transform2 transform, Action onClick) : base(10, transform.ToRectangle())
        {
            _image = image;
            _default = new RectangleTexture(transform.Size, Color.Transparent).Create();
            _hover = new RectangleTexture(transform.Size, hover).Create();
            _press = new RectangleTexture(transform.Size, hover).Create();
            _transform = transform;
            _onClick = onClick;
            _current = _default;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_image, parentTransform + _transform);
            World.Draw(_current, parentTransform + _transform);
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
    }
}
