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
        public static readonly ClickableUIElement None = new NoneClickableUIElement();

        private readonly List<ClickUILayer> _layers = new List<ClickUILayer> { new ClickUILayer() };

        private ClickableUIElement _currentElement = None;
        private bool _wasClicked;

        public void Add(ClickUILayer layer)
        {
            Add(layer, _layers.Count);
        }

        public void Add(ClickUILayer layer, int priority)
        {
            for (var i = _layers.Count - 1; i < priority; i++)
                _layers.Add(new ClickUILayer());
            _layers.RemoveAt(priority);
            _layers.Insert(priority, layer);
        }

        public void Add(ClickableUIElement element)
        {
            _layers[0].Add(element);
        }

        public void Remove(ClickUILayer layer)
        {
            Remove(_layers.IndexOf(layer));
        }

        public void Remove(int priority)
        {
            for (var i = _layers.Count - 1; i < priority; i++)
                _layers.Add(new ClickUILayer());
            _layers.RemoveAt(priority);
            _layers.Insert(priority, new ClickUILayer());
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
            _currentElement.OnEntered();
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
            var element = _layers.LastOrDefault(x => x.GetElement(mousePosition) != None)?.GetElement(mousePosition);
            return element ?? None;
        }
    }
}
