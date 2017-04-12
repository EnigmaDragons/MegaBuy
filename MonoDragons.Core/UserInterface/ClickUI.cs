using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ClickUI : IVisualAutomaton
    {
        public static readonly ClickableUIElement None = new NoneClickableUIElement();

        private readonly List<ClickUILayer> _layers = new List<ClickUILayer> { new ClickUILayer("Default") };

        private readonly ColoredRectangle _elementHighlight = new ColoredRectangle { Color = Color.Transparent };
        private ClickableUIElement _currentElement = None;
        private bool _wasClicked;
        private ClickUILayer _elementLayer;
        private bool _elementChangeAfterPressed;

        public ClickUI()
        {
            _elementLayer = _layers[0];
        }

        public void Add(ClickUILayer layer)
        {
            if(!_layers.Contains(layer))
                _layers.Add(layer);
        }

        public void Add(ClickUILayer layer, int priority)
        {
            _layers.Insert(priority, layer);
        }

        public void Add(ClickableUIElement element)
        {
            _layers[0].Add(element);
        }

        public void Remove(ClickUILayer layer)
        {
            _layers.Remove(layer);
        }

        public void Remove(int index)
        {
            _layers.RemoveAt(index);
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
            if (_wasClicked)
                return;

            _currentElement.OnPressed();
            _wasClicked = true;
        }

        private void OnReleased()
        {
            _currentElement.OnReleased();
            _wasClicked = false;
            _elementChangeAfterPressed = false;
            _currentElement.OnEntered();
        }

        private void ChangeActiveElement(ClickableUIElement newElement)
        {
            _elementChangeAfterPressed = _wasClicked;
            _currentElement.OnExitted();
            _wasClicked = false;
            _currentElement = newElement;
            _currentElement.OnEntered();
            _elementHighlight.Transform = new Transform2(_currentElement.Area);
            _elementHighlight.Color = Color.FromNonPremultiplied(100, 100, 0, 30);
        }

        private bool WasMouseReleased(MouseState mouse)
        {
            return _wasClicked 
                && mouse.LeftButton == ButtonState.Released 
                && new Rectangle(_currentElement.Area.Location + _elementLayer.Location.ToPoint(), _currentElement.Area.Size).Contains(mouse.Position);
        }

        private ClickableUIElement GetElement(Point mousePosition)
        {
            for (var i = _layers.Count - 1; i >= 0; i--)
                if (_layers[i].GetElement(mousePosition) != None)
                {
                    _elementLayer = _layers[i];
                    return _layers[i].GetElement(mousePosition);
                }
            return None;
        }

        public void Draw(Transform2 parentTransform)
        {
#if DEBUG
            _elementHighlight?.Draw(parentTransform + _elementLayer.Location);
            UI.DrawText($"Mouse: {Mouse.GetState().Position}", new Vector2(1200, 800), Color.Yellow);
            UI.DrawText($"Clicked: {_wasClicked}, {_elementChangeAfterPressed}", new Vector2(1200, 840), Color.Yellow);
#endif
        }
    }
}
