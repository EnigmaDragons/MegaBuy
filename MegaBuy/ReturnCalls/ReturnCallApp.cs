using System;
using System.Collections.Generic;
using MegaBuy.Pads.Apps;
using MegaBuy.ReturnCalls.Callers;
using MegaBuy.ReturnCalls.Messages;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls
{
    public class ReturnCallApp : IApp
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automatons = new List<IAutomaton>();

        public App Type => App.Call;
        public ClickUIBranch Branch { get; }

        public ReturnCallApp()
        {
            //Build Branch
            Branch = new ClickUIBranch("Call App", (int)ClickUIPriorities.Pad);

            //Build Grids
            var grid = new GridLayout(new Size2(1600, 695), 2, 1);
            var messengerGrid = new GridLayout(grid.GetBlockSize(1, 1), 1, 
                new List<Definition> { new ShareDefintion(), new ConcreteDefinition(95)});
            var callerGrid = new GridLayout(grid.GetBlockSize(2, 1), 
                new List<Definition> { new ShareDefintion(), new ConcreteDefinition(300) }, 
                new List<Definition> { new ConcreteDefinition(150), new ConcreteDefinition(150), new ConcreteDefinition(150), new ShareDefintion() });

            //Build Pieces
            var messengerTransform = new Transform2(messengerGrid.GetBlockSize(1, 1) - new Size2(50, 50));
            var messenger = new ReturnsCallMessengerUI(messengerTransform);
            var excuseButton = ImageTextButtonFactory.Create("Excuse", new Vector2(0, 0), () => { });

            var caller = new CallerFaceUI();
            var ready = new ReturnsCallReadyUI();

            //Griding
            grid.AddSpatial(messengerGrid, new Transform2(messengerGrid.Size), 1, 1);
            grid.AddSpatial(callerGrid, new Transform2(callerGrid.Size), 2, 1);

            messengerGrid.AddSpatial(messenger, messengerTransform, 1, 1);
            messengerGrid.AddSpatial(excuseButton, excuseButton.Transform, 1, 2);

            callerGrid.AddSpatial(caller, caller.Transform, 2, 1, 1, 3);
            callerGrid.AddSpatial(ready, ready.Transform, 1, 1, 3, 1);

            //Branching
            Branch.Add(excuseButton);
            Branch.Add(ready.Branch);

            //Adding
            _visuals.Add(grid);
            _automatons.Add(messenger);
        }

        public void Update(TimeSpan delta)
        {
            _automatons.ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
