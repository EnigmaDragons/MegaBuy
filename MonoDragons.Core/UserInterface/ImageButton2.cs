using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ImageButton2 : ClickableUIElement, IVisual
    {
        private readonly string _basic;
        private readonly string _hover;
        private readonly string _pressed;
        private readonly Transform2 _transform;
        
        private string _current;

        public ImageButton2(string basic, string hover, string pressed, Transform2 transform) 
            : base(10, transform.ToRectangle())
        {
            _basic = basic;
            _hover = hover;
            _pressed = pressed;
            _transform = transform;
            _current = _basic;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_current, parentTransform + _transform);
        }

        public override void OnEntered()
        {
            _current = _hover;
        }

        public override void OnExitted()
        {
            _current = _basic;
        }

        public override void OnPressed()
        {
            _current = _pressed;
        }

        public override void OnReleased()
        {
            _current = _basic;
        }
    }
}
