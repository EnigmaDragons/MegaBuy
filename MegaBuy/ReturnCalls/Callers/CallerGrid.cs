using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.Callers
{
    public class CallerGrid : IVisual
    {
        private List<IVisual> _visuals = new List<IVisual>();

        public CallerGrid(Size2 size)
        {
            var callerGrid = new GridLayout(size, 1,
                new List<Definition>
                {
                    new ConcreteDefinition(25),
                    new ConcreteDefinition(410),
                    new ConcreteDefinition(70),
                    new ShareDefintion()
                });

            var caller = new CallerFaceUI();
            var name = new CallerNameUI();

            callerGrid.AddSpatial(caller, caller.Transform, 1, 2);
            callerGrid.AddSpatial(name, name.Transform, 1, 3);

            _visuals.Add(callerGrid);
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
