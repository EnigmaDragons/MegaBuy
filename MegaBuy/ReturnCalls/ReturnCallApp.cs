using System;
using System.Collections.Generic;
using MegaBuy.Calls.Ratings;
using MegaBuy.Pads.Apps;
using MegaBuy.ReturnCalls.Callers;
using MegaBuy.ReturnCalls.Choices;
using MegaBuy.ReturnCalls.Messages;
using MegaBuy.ReturnCalls.PurchaseHistories;
using MegaBuy.ReturnCalls.Ratings;
using MegaBuy.UIs;
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
            Branch = new ClickUIBranch("Call App", (int)ClickUIPriorities.Pad);
            var grid = new GridLayout(new Size2(1600, 720), 2, 1);
            
            var messengerGrid = new MessengerGrid(grid.GetBlockSize(1, 1));
            var callerGrid = new CallerGrid(grid.GetBlockSize(2, 1));
            var rating = new ReturnsRatingsUI();
            var purchaseHistoryGrid = new PurchaseHistoryGrid(grid.GetBlockSize(2, 1));

            grid.AddSpatial(messengerGrid, new Transform2(grid.GetBlockSize(1, 1)), 1, 1);
            grid.AddSpatial(rating, rating.Transform, 1, 1);
            grid.AddSpatial(callerGrid, new Transform2(grid.GetBlockSize(2, 1)), 2, 1);
            grid.AddSpatial(purchaseHistoryGrid, new Transform2(grid.GetBlockSize(2, 1)), 2, 1);

            Branch.Add(messengerGrid.Branch);
            Branch.Add(callerGrid.Branch);
            Branch.Add(purchaseHistoryGrid.Branch);
            _visuals.Add(grid);
            _automatons.Add(messengerGrid);
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
