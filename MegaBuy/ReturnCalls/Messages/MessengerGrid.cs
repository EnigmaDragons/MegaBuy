using System;
using System.Collections.Generic;
using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.Messages
{
    public class MessengerGrid : IVIsualAutomatonControl
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automatons = new List<IAutomaton>();
        private readonly List<ClickUIBranch> _branches = new List<ClickUIBranch>();

        private bool _isInCall = false;

        public ClickUIBranch Branch { get; }

        public MessengerGrid(Size2 size)
        {
            Branch = new ClickUIBranch("Messenger", (int)ClickUIPriorities.Pad);

            var grid = new GridLayout(size, 1, new List<Definition> { new ShareDefintion(), new ConcreteDefinition(70) });

            var messengerTransform = new Transform2(grid.GetBlockSize(1, 1) - new Size2(50, 50));
            var messenger = new ReturnsCallMessengerUI(messengerTransform);
            var excuses = new ExcusesUI();

            grid.AddSpatial(messenger, messengerTransform, 1, 1);
            grid.AddSpatial(excuses, excuses.Transform, 1, 2);

            _automatons.Add(messenger);
            _visuals.Add(grid);
            _branches.Add(excuses.Branch);

            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Update(TimeSpan delta)
        {
            _automatons.ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isInCall)
                _visuals.ForEach(x => x.Draw(parentTransform));
        }

        private void OnCallStart()
        {
            _branches.ForEach(x => Branch.Add(x));
            _isInCall = true;
        }

        private void OnCallResolved()
        {
            Branch.ClearElements();
            _isInCall = false;
        } 
    }
}
