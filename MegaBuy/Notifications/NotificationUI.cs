using System.Collections.Generic;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Notifications
{
    public sealed class NotificationUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.Margin, 0));
        private readonly Label _label;
        private readonly ImageTextButton _button;

        public ClickUIBranch Branch { get; }

        public NotificationUI(PlayerNotification notification, List<NotificationUI> items)
        {
            Branch = new ClickUIBranch("Notification", (int)ClickUIPriorities.Pad);
            _label = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Transform = new Transform2(new Vector2(Sizes.Margin, 0), new Size2(Sizes.Notification.Width - Sizes.Button.Width - Sizes.Margin * 2, 90)),
                Text = $"{notification.Time} - {notification.Sender} - {notification.Message}",
            };
            _button = ImageTextButtonFactory.Create("Dismiss", new Vector2(Sizes.Notification.Width - Sizes.Margin - Sizes.Button.Width, Sizes.SmallMargin), () => items.Remove(this));
            Branch.Add(_button);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            World.Draw("Images/UI/notification", absoluteTransform + new Transform2(Sizes.Notification));
            _label.Draw(absoluteTransform);
            _button.Draw(absoluteTransform);
        }
    }
}
