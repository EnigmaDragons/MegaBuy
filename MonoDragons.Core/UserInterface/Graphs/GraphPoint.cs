using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Graphs
{
    public class GraphPoint : IVisual
    {
        private readonly Texture2D _pointTexture;

        public decimal X { get; }
        public decimal Y { get; }
        public Size2 Size { get; }

        public GraphPoint(decimal x, decimal y)  : this(x, y, GraphDefaults.GraphPointSize, GraphDefaults.GraphPoint()) {}

        public GraphPoint(decimal x, decimal y, Size2 size, Texture2D pointTexture)
        {
            _pointTexture = pointTexture;
            X = x;
            Y = y;
            Size = size;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_pointTexture, new Transform2(Size) + parentTransform);
        }
    }
}
