using System;
using System.Collections.Generic;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Notifications
{
    public sealed class NotificationApp : IApp
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly List<NotificationUI> _notifications = new List<NotificationUI>();
        
        public App Type => App.Notification;
        public ClickUIBranch Branch { get; }

        public NotificationApp()
        {
            Branch = new ClickUIBranch("Notification App", (int)ClickUIPriorities.Pad);
            OnNotificationReceived(new PlayerNotification("MegaBuy", "Congratulations! Your notification app has been installed."));
            World.Subscribe(EventSubscription.Create<PlayerNotification>(OnNotificationReceived, this));
        }

        private void OnNotificationReceived(PlayerNotification obj)
        {
            var ui = new NotificationUI(obj, _notifications);
            _notifications.Add(ui);
            Branch.Add(ui.Branch);
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.Location = absoluteTransform.Location;
            for (var i = 0; i < _notifications.Count; i++)
                _notifications[i].Draw(absoluteTransform + new Transform2(new Vector2(0, Sizes.Margin + i * (90 + Sizes.SmallMargin))));
        }
    }
}
