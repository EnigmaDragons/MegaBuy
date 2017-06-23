using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Graphs
{
    public class GraphGrid : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public GraphGrid(Size2 size, int xIntervals, int yIntervals)
        {
            var intervalWidth = size.Width / (xIntervals - 1);
            var intervalHeight = size.Height / (yIntervals - 1);
            for (var column = 0; column < yIntervals; column++)
                _visuals.Add(new Line(new Vector2(intervalWidth * column, 0), new Vector2(intervalWidth * column, size.Height), GraphDefaults.GraphBackgroundLine()));
            for (var row = 0; row < yIntervals; row++)
                _visuals.Add(new Line(new Vector2(0, intervalHeight * row), new Vector2(size.Width, intervalHeight * row), GraphDefaults.GraphBackgroundLine()));
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
