using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Player.Thoughts
{
    public sealed class ThoughtUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.Margin, 0));
        private readonly Label _label;
        private readonly ImageTextButton _button;

        public ClickUIBranch Branch { get; }

        public ThoughtUI(string text)
        {
            Branch = new ClickUIBranch("Thought", (int)ClickUIPriorities.Thoughts);
            _label = new Label
            {
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                Text = text,
                Transform = new Transform2(new Vector2(Sizes.Margin, 0),
                    new Size2(Sizes.Notification.Width - Sizes.Button.Width - Sizes.Margin * 2, 90))
            };
            _button = ImageTextButtonFactory.Create("Dismiss",
                new Vector2(Sizes.Notification.Width - Sizes.Margin - Sizes.Button.Width, Sizes.SmallMargin),
                null);  // TODO: This is bad, I know, just don't know
                        // if I can get away with not having the 
                        // list of items like the notificationUI has
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