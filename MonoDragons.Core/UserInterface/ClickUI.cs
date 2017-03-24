using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ClickUI : IAutomaton
    {
        private static readonly ClickableUIElement None = new NoneClickableUIElement();

        private List<ClickableUIElement> _elements = new List<ClickableUIElement>();

        private ClickableUIElement _currentElement = None;
        private bool _wasClicked;

        public void Add(ClickableUIElement element)
        {
            _elements.Add(element);
            _elements = _elements.OrderByDescending(x => x.Layer).ToList();
        }

        public void Remove(ClickableUIElement element)
        {
            _elements.Remove(element);
        }

        public void Update(TimeSpan delta)
        {
            var mouse = Mouse.GetState();
            var newElement = GetElement(mouse.Position);
            if (newElement != _currentElement)
                ChangeActiveElement(newElement);
            else if (WasMouseReleased(mouse))
                OnReleased();
            else if (mouse.LeftButton == ButtonState.Pressed)
                OnPressed();
        }

        private void OnPressed()
        {
            _currentElement.OnPressed();
            _wasClicked = true;
        }

        private void OnReleased()
        {
            _currentElement.OnReleased();
            _wasClicked = false;
        }

        private void ChangeActiveElement(ClickableUIElement newElement)
        {
            _currentElement.OnExitted();
            _wasClicked = false;
            _currentElement = newElement;
            _currentElement.OnEntered();
        }

        private bool WasMouseReleased(MouseState mouse)
        {
            return _wasClicked && mouse.LeftButton == ButtonState.Released && _currentElement.Area.Contains(mouse.Position);
        }

        private ClickableUIElement GetElement(Point mousePosition)
        {
            var element = _elements.FirstOrDefault(x => x.Area.Contains(mousePosition));
            return element ?? None;
        }
    }
}
