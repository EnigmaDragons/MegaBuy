using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Notifications.Old
{
    public sealed class NotificationUI : IVisual
    {
        private readonly Label _label;

        public IconButton Button { get; }

        public NotificationUI(PlayerNotification notification, List<NotificationUI> items)
        {
            _label = new Label
            {
                Text = $"{notification.Time} - {notification.Sender} - {notification.Message}",
                Transform = new Transform2(new Size2(900, 100))
            };
            Button = new IconButton("Images/Icons/dismiss", new Rectangle(10, 10, 80, 80), new Rectangle(900, 0, 100, 100), 
                Color.White, Color.Gray, Color.Black, () => items.Remove(this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _label.Draw(parentTransform);
            Button.Draw(parentTransform);
        }
    }
}
