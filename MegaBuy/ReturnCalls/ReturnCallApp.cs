using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Pads.Apps;
using MegaBuy.ReturnCalls.Callers;
using MegaBuy.ReturnCalls.Choices;
using MegaBuy.ReturnCalls.Messages;
using MegaBuy.ReturnCalls.PurchaseHistories;
using MegaBuy.ReturnCalls.Ratings;
using MegaBuy.ReturnCalls.Ready;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
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
            var grid = new GridLayout(new Size2(1600, 720), 
                new List<Definition>
                {
                    new ConcreteDefinition(25),
                    new ConcreteDefinition(250),
                    new ConcreteDefinition(525),
                    new ShareDefintion()
                }, 
                new List<Definition>
                {
                    new ShareDefintion(),
                    new ConcreteDefinition(70)
                });
            var innerGrid = new GridLayout(new Size2(1600, 720), 1, 2);

            var callerGrid = new CallerGrid(grid.GetBlockSize(2, 1));
            var messengerGrid = new MessengerGrid(grid.GetBlockSize(3, 1));
            var purchaseHistoryGrid = new PurchaseHistoryGrid(grid.GetBlockSize(4, 1));
            var rating = new ReturnsRatingsUI();
            var ready = new ReturnsCallReadyUI();
            var excuses = new ExcusesUI();
            var callChoicesTransform = new Transform2(grid.GetBlockSize(4, 2));
            var callChoices = new ChoicesUI(callChoicesTransform);

            grid.AddSpatial(callerGrid, new Transform2(grid.GetBlockSize(2, 1)), 2, 1);
            grid.AddSpatial(messengerGrid, new Transform2(grid.GetBlockSize(3, 1)), 3, 1);
            grid.AddSpatial(purchaseHistoryGrid, new Transform2(grid.GetBlockSize(4, 1)), 4, 1);
            innerGrid.AddSpatial(rating, rating.Transform, 1, 1);
            innerGrid.AddSpatial(ready, ready.Transform, 1, 2);
            grid.AddSpatial(callChoices, callChoicesTransform, 4, 2);
            grid.AddSpatial(excuses, excuses.Transform, 3, 2);

            Branch.Add(callChoices.Branch);
            Branch.Add(purchaseHistoryGrid.Branch);
            Branch.Add(ready.Branch);
            Branch.Add(excuses.Branch);
            _visuals.Add(grid);
            _visuals.Add(innerGrid);
            _automatons.Add(messengerGrid);
            World.Subscribe(EventSubscription.Create<CallStarted>(CallStarted, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(CallResolved, this));
        }

        public void Update(TimeSpan delta)
        {
            _automatons.ToList().ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        private void CallStarted(CallStarted obj)
        {
            _automatons.Add(obj.Call);
        }

        private void CallResolved(CallResolved obj)
        {
            _automatons.RemoveAll(x => x is Call);
        }
    }
}
