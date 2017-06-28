using System;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace MegaBuy.ReturnCalls.BetweenCalls
{
    public class Spinner : ISpatialVisual, IAutomaton
    {
        private readonly Animation _spinner = new Animation(10, Enumerable.Range(1, 36).Select(x => "Images/Spinner/spinner-" + x).ToArray());

        private bool _isConnecting = false;

        public Transform2 Transform { get; } = new Transform2(new Size2(200, 200));

        public Spinner()
        {
            World.Subscribe(EventSubscription.Create<AgentCallStatusChanged>(x => OnAgentCallStatusChanged(x.Status), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isConnecting)
                _spinner.Draw(parentTransform + Transform);
        }

        public void OnAgentCallStatusChanged(AgentCallStatus status)
        {
            if (status == AgentCallStatus.Available)
                _isConnecting = true;
            if (status == AgentCallStatus.InCall)
                _isConnecting = false;
        }

        public void Update(TimeSpan delta)
        {
            _spinner.Update(delta);
        }
    }
}
