﻿
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls
{
    public sealed class AgentCurrentCallStatus
    {
        private AgentCallStatus _status = AgentCallStatus.Offline;

        public bool IsAvailable => _status.Equals(AgentCallStatus.Available);

        public AgentCurrentCallStatus()
        {
            World.Subscribe(new EventSubscription<AgentCallStatusChanged>(UpdateStatus, this));
            World.Subscribe(new EventSubscription<CallStarted>(CallStarted, this));
        }

        private void UpdateStatus(AgentCallStatusChanged statusEvent)
        {
            _status = statusEvent.Status;
        }

        private void CallStarted(CallStarted callStarted)
        {
            _status = AgentCallStatus.InCall;
        }
    }
}
