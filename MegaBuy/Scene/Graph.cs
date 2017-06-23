using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface.Graphs;

namespace MegaBuy.Scene
{
    public class Graph : IScene
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public void Init()
        {
            _visuals.Add(new GraphLine(8, 8, new Size2(1600, 900), 
                new GraphPoint(0, 0), new GraphPoint(1, 1), new GraphPoint(2, (decimal)1.5), 
                new GraphPoint(3, (decimal)4.61), new GraphPoint(4, (decimal)3.27), new GraphPoint(5, (decimal)6.1),
                new GraphPoint(7, (decimal)(6.2))));
            _visuals.Add(new GraphGrid(new Size2(1600, 900), 8, 8));
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            _visuals.ForEach(x => x.Draw(Transform2.Zero));
        }
    }
}
