using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Graphs
{
    public class GraphLine : IVisual
    {
        private readonly int _xIntervals;
        private readonly int _yIntervals;
        private readonly Size2 _size;
        private readonly Texture2D _line;
        private readonly List<GraphPoint> _points;
        private readonly Dictionary<IVisual, Transform2> _visuals = new Dictionary<IVisual, Transform2>();
        
        private int _intervalWidth;
        private int _intervalHeight;
        private bool _initialized = false;

        public GraphLine(int xIntervals, int yIntervals, Size2 size, params GraphPoint[] points) 
            : this(xIntervals, yIntervals, size, GraphDefaults.GraphLine(), points) {}

        public GraphLine(int xIntervals, int yIntervals, Size2 size, Texture2D line, params GraphPoint[] points)
        {
            _xIntervals = xIntervals;
            _yIntervals = yIntervals;
            _size = size;
            _line = line;
            _points = points.ToList();
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_initialized)
                Initialize();
            _visuals.ForEach(x => x.Key.Draw(parentTransform + x.Value));
        }

        private void Initialize()
        {
            _intervalWidth = _size.Width / (_xIntervals - 1);
            _intervalHeight = _size.Height / (_yIntervals - 1);
            Dictionary<GraphPoint, Transform2> _placedGraphPoints = new Dictionary<GraphPoint, Transform2>();
            Transform2 lastPointTransform = null; 
            for (var i = 0; i < _points.Count; i++)
            {
                var point = _points[i];
                var pointTransform = new Transform2(new Vector2((float)(_intervalWidth * point.X), _size.Height - (float)(_intervalHeight * point.Y)));
                _placedGraphPoints[point] = pointTransform;
                if (i != 0)
                    _visuals.Add(new Line(lastPointTransform.Location, pointTransform.Location, _line), Transform2.Zero);
                lastPointTransform = pointTransform;
            }
            _placedGraphPoints.ForEachIndex((x, i) => _visuals.Add(x.Key, x.Value + new Transform2(new Vector2(-_points[0].Size.Width / 2, -_points[0].Size.Height / 2))));
            _initialized = true;
        }
    }
}
