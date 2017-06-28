using System;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.BetweenCalls
{
    public class BetweenCallGrid : IVisualControl, IAutomaton
    {
        private readonly GridLayout _grid;
        private readonly Spinner _spinner;

        public ClickUIBranch Branch { get; }

        public BetweenCallGrid(Size2 size)
        {
            Branch = new ClickUIBranch("BetweenCalls", (int)ClickUIPriorities.Pad);
            _grid = new GridLayout(size, 1, 3);

            var rating = new ReturnsRatingsUI();
            var ready = new ReturnsCallReadyUI(); 
            var summaries = new CallSummaryUI();
            _spinner = new Spinner();

            _grid.AddSpatial(rating, rating.Transform, 1, 1);
            _grid.AddSpatial(ready, ready.Transform, 1, 2);
            _grid.AddSpatial(summaries, summaries.Transform, 1, 3);
            _grid.AddSpatial(_spinner, _spinner.Transform, 1, 2);

            Branch.Add(ready.Branch);
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            _grid.Draw(parentTransform);
        }

        public void Update(TimeSpan delta)
        {
            _spinner.Update(delta);
        }
    }
}
