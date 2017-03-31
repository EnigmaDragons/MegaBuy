﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.UserInterface
{
    public class ClickUILayer
    {
        private List<ClickableUIElement> _elements = new List<ClickableUIElement>();

        public Vector2 Location { get; set; }

        public void Add(ClickableUIElement element)
        {
            _elements.Add(element);
            _elements = _elements.OrderByDescending(x => x.Layer).ToList();
        }

        public void Remove(ClickableUIElement element)
        {
            _elements.Remove(element);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public ClickableUIElement GetElement(Point mousePosition)
        {
            var element = _elements.FirstOrDefault(x => x.Area.Contains(mousePosition - Location.ToPoint()));
            return element ?? ClickUI.None;
        }
    }
}
