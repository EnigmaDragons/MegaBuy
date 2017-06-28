using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls.Events;
using MegaBuy.Pads.Apps;
using MegaBuy.ReturnCalls.BetweenCalls;
using MegaBuy.ReturnCalls.Callers;
using MegaBuy.ReturnCalls.Choices;
using MegaBuy.ReturnCalls.Messages;
using MegaBuy.ReturnCalls.PurchaseHistories;
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
        private readonly GridLayout _callGrid;
        private readonly ClickUIBranch _callBranch;
        private readonly List<IAutomaton> _automatons = new List<IAutomaton>();
        private readonly ClickUIBranch _betweenCallBranch;
        private readonly BetweenCallGrid _betweenCallGrid;

        private IVisual _toDraw;
        
        public App Type => App.Call;
        public ClickUIBranch Branch { get; }

        public ReturnCallApp()
        {
            Branch = new ClickUIBranch("Call App", (int)ClickUIPriorities.Pad);
            _callBranch = new ClickUIBranch("In Call Branch", (int)ClickUIPriorities.Pad);
            _betweenCallBranch = new ClickUIBranch("Between Call Branch", (int)ClickUIPriorities.Pad);

            _callGrid = new GridLayout(new Size2(1600, 720), 
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

            var callerGrid = new CallerGrid(_callGrid.GetBlockSize(2, 1));
            var messengerGrid = new MessengerGrid(_callGrid.GetBlockSize(3, 1));
            var purchaseHistoryGrid = new PurchaseHistoryGrid(_callGrid.GetBlockSize(4, 1));
            _betweenCallGrid = new BetweenCallGrid(_callGrid.GetBlockSize(1, 1, 4, 2));
            var excuses = new ExcusesUI();
            var callChoicesTransform = new Transform2(_callGrid.GetBlockSize(4, 2));
            var callChoices = new ChoicesUI(callChoicesTransform);

            _callGrid.AddSpatial(callerGrid, new Transform2(_callGrid.GetBlockSize(2, 1)), 2, 1);
            _callGrid.AddSpatial(messengerGrid, new Transform2(_callGrid.GetBlockSize(3, 1)), 3, 1);
            _callGrid.AddSpatial(purchaseHistoryGrid, new Transform2(_callGrid.GetBlockSize(4, 1)), 4, 1);
            _callGrid.AddSpatial(callChoices, callChoicesTransform, 4, 2);
            _callGrid.AddSpatial(excuses, excuses.Transform, 3, 2);

            _callBranch.Add(callChoices.Branch);
            _callBranch.Add(purchaseHistoryGrid.Branch);
            _callBranch.Add(excuses.Branch);
            _betweenCallBranch.Add(_betweenCallGrid.Branch);
            _automatons.Add(messengerGrid);
            _automatons.Add(callerGrid);
            _automatons.Add(_betweenCallGrid);

            _toDraw = _betweenCallGrid;
            Branch.Add(_betweenCallBranch);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallEnd(), this));
        }

        public void Update(TimeSpan delta)
        {
            _automatons.ToList().ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _toDraw.Draw(parentTransform);
        }

        private void OnCallStart()
        {
            Branch.Remove(_betweenCallBranch);
            Branch.Add(_callBranch);
            _toDraw = _callGrid;
        }

        private void OnCallEnd()
        {
            Branch.Remove(_callBranch);
            Branch.Add(_betweenCallBranch);
            _toDraw = _betweenCallGrid;
        }
    }
}
