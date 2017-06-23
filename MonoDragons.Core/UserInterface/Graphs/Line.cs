using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Graphs
{
    public class Line : IVisual
    {
        private readonly Vector2 _point1;
        private readonly Vector2 _point2;
        private readonly Texture2D _line;

        public Line(Vector2 point1, Vector2 point2, Texture2D line)
        {
            _point1 = point1;
            _point2 = point2;
            _line = line;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.DrawLine(_line, _point1 + parentTransform.Location, _point2 + parentTransform.Location);
        }
    }
}
