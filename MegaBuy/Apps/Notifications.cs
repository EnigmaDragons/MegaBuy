using System;
using MegaBuy.Notifications;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Apps
{
    public class Notifications : IVisualAutomaton
    {
        public Notifications()
        {
            World.Subscribe(new EventSubscription<PlayerNotification>(OnNotificationReceived, this));
        }

        private void OnNotificationReceived(PlayerNotification obj)
        {
            throw new NotImplementedException();
        }

        public void Update(TimeSpan delta)
        {
            throw new NotImplementedException();
        }

        public void Draw(Transform2 parentTransform)
        {
            throw new NotImplementedException();
        }
    }
}