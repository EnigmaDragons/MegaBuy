using MegaBuy.UIs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.BetweenCalls
{
    public class BetweenCallGrid : IVisualControl
    {
        private readonly GridLayout _grid;

        public ClickUIBranch Branch { get; }

        public BetweenCallGrid(Size2 size)
        {
            Branch = new ClickUIBranch("BetweenCalls", (int)ClickUIPriorities.Pad);
            _grid = new GridLayout(size, 1, 3);

            var rating = new ReturnsRatingsUI();
            var ready = new ReturnsCallReadyUI(); 
            var summaries = new CallSummaryUI();

            _grid.AddSpatial(rating, rating.Transform, 1, 1);
            _grid.AddSpatial(ready, ready.Transform, 1, 2);
            _grid.AddSpatial(summaries, summaries.Transform, 1, 3);

            Branch.Add(ready.Branch);
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            _grid.Draw(parentTransform);
        }
    }
}
