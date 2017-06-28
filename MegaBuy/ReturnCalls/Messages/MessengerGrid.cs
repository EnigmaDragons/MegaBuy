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
    public class MessengerGrid : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automatons = new List<IAutomaton>();

        public MessengerGrid(Size2 size)
        {
            var grid = new GridLayout(size, 1, 1);

            var messengerTransform = new Transform2(grid.GetBlockSize(1, 1) - new Size2(50, 50));
            var messenger = new ReturnsCallMessengerUI(messengerTransform);

            grid.AddSpatial(messenger, messengerTransform, 1, 1);

            _automatons.Add(messenger);
            _visuals.Add(grid);
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
