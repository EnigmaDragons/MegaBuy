using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Render
{
    public sealed class MutableDrawnText : IVisual
    {
        private readonly Color _color;

        private string _text = "";

        public MutableDrawnText(Color color)
        {
            _color = color;
        }

        public void Set(string text)
        {
            _text = text;
        }

        public void Draw(Transform parentTransform)
        {
            UI.DrawText(_text, parentTransform.Location, _color);
        }
    }
}
