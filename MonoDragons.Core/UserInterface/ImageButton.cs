using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ImageButton : IVisualAutomaton
    {
        private readonly string _basic;
        private readonly string _hover;
        private readonly string _pressed;
        private readonly Transform2 _transform;
        private readonly Action _onClick;

        private string _current;
        private bool _wasClicked;

        public ImageButton(string basic, string hover, string pressed, Transform2 transform, Action onClick)
        {
            _basic = basic;
            _hover = hover;
            _pressed = pressed;
            _transform = transform;
            _onClick = onClick;
            _current = _basic;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_current, _transform + parentTransform);
        }

        public void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();
            if (_transform.ToRectangle().Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
                SetClicked();
            else if (_transform.ToRectangle().Contains(mouse.Position))
                _current = _hover;
            else
                _current = _basic;
            CheckForReleased(mouse);
        }

        private void CheckForReleased(MouseState mouse)
        {
            if (!_wasClicked || mouse.LeftButton != ButtonState.Released) return;

            _wasClicked = false;
            _onClick();
        }

        private void SetClicked()
        {
            _wasClicked = true;
            _current = _pressed;
        }
    }
}
