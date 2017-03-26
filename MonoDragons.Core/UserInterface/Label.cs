using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class Label : IVisual
    {
        private readonly Color _color;

        private string _text = "";

        public Label(Color color)
        {
            _color = color;
        }

        public void Set(string text)
        {
            _text = text;
        }

        public void Draw(Transform2 parentTransform)
        {
            UI.DrawText(_text, parentTransform.Location, _color);
        }
    }
}
