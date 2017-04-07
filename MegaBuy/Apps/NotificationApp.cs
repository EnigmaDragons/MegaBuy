using System;
using System.Collections.Generic;
using MegaBuy.Notifications;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    public sealed class NotificationApp : IApp
    {
        private readonly ClickUI _clickUi;
        private readonly ClickUILayer _layer;
        private readonly List<NotificationUI> _notifications = new List<NotificationUI>();
        private int _notificationCount;

        public App Type => App.Notification;

        private readonly ColoredRectangle _rect = new ColoredRectangle { Transform = new Transform2(new Size2(1920, 1080)) };

        public NotificationApp(ClickUI clickUi)
        {
            _layer = new ClickUILayer("NotificationApp");
            _clickUi = clickUi;
            _notifications.Add(new NotificationUI(new PlayerNotification("MegaBuy", "Congratulations! Your notification app has been installed."), _notifications));
            World.Subscribe(new EventSubscription<PlayerNotification>(OnNotificationReceived, this));
        }

        private void OnNotificationReceived(PlayerNotification obj)
        {
            _notifications.Add(new NotificationUI(obj, _notifications));
            _notificationCount = _notifications.Count;
            _layer.Add(new OffsetClickableUIElement(_notifications[_notificationCount - 1].Button, new Point(0, (_notificationCount - 1) * 110)));
        }

        public void Update(TimeSpan delta)
        {
            if (_notificationCount == _notifications.Count)
                return;

            _notificationCount = _notifications.Count;
            _layer.Clear();
            for (var i = 0; i < _notifications.Count; i++)
                _layer.Add(new OffsetClickableUIElement(_notifications[i].Button, new Point(0, i * 110)));
        }

        public void Draw(Transform2 parentTransform)
        {
            _layer.Location = parentTransform.Location;
            _rect.Draw(parentTransform);
            for (var i = 0; i < _notifications.Count; i++)
                _notifications[i].Draw(parentTransform + new Transform2(new Vector2(0, i * 110)));
        }

        public void LostFocus()
        {
            _clickUi.Remove(_layer);
        }

        public void GainedFocus()
        {
            _clickUi.Add(_layer);
        }
    }
}
