using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Player.Thoughts
{
    public sealed class ThoughtUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(800 - Sizes.LargeLabel.Width / 2, 450 - Sizes.LargeLabel.Height / 2));
        private readonly Label _label;
        private readonly ImageTextButton _button;

        private bool _isThinking = false;
        private bool _justReceivedThought = false;

        public ClickUIBranch Branch { get; }

        public ThoughtUI()
        {
            Input.On(Control.A, Dismiss);
            Branch = new ClickUIBranch("Thought", (int)ClickUIPriorities.Thoughts);
            _label = new Label
            {
                TextColor = Color.White,
                BackgroundColor = Color.Transparent,
            };
            _button = ImageTextButtonFactory.Create("Dismiss", 
                new Vector2(Sizes.LargeLabel.Width - Sizes.SmallMargin - Sizes.Button.Width, Sizes.LargeLabel.Height - Sizes.SmallMargin - Sizes.Button.Height), 
                Dismiss);
            World.Subscribe(EventSubscription.Create<HadAThought>(Think, this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isThinking)
                return;
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            World.Draw("Images/UI/label-large", absoluteTransform + new Transform2(Sizes.LargeLabel));
            _label.Draw(absoluteTransform);
            _button.Draw(absoluteTransform);
        }

        private void Think(HadAThought thought)
        {
            Branch.Add(_button);
            _label.Transform = new Transform2(new Size2(Sizes.LargeLabel.Width - Sizes.SmallMargin * 2, Sizes.LargeLabel.Height - Sizes.SmallMargin * 2));
            _label.Text = thought.Thought;
            var font = Resources.Load<SpriteFont>(_label.Font);
            var size = font.MeasureString(_label.Text);
            _label.Transform = new Transform2(new Vector2(Sizes.SmallMargin, Sizes.SmallMargin), new Size2((int)size.X, (int)size.Y));
            _justReceivedThought = true;
            _isThinking = true;
            World.Publish(new InteractionStarted());
        }

        private void Dismiss()
        {
            if (!_isThinking)
                return;

            if (_justReceivedThought)
            {
                _justReceivedThought = false;
                return;
            }
            
            Branch.Remove(_button);
            _isThinking = false;
            World.Publish(new InteractionFinished());
        }
    }
}