using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class Label : IVisual
    {
        private readonly ColoredRectangle _background = new ColoredRectangle();

        public string Font { get; set; } = "Fonts/Arial";
        public Color TextColor { get; set; } = Color.White;
        public string Text { get; set; } = "";

        public Transform2 Transform
        {
            get { return _background.Transform; }
            set { _background.Transform = value; }
        }

        public Color BackgroundColor
        {
            get { return _background.Color; }
            set { _background.Color = value; }
        }

        public void Draw(Transform2 parentTransform)
        {
            var position = parentTransform + Transform;
            _background.Draw(parentTransform);
            var size = Resources.Load<SpriteFont>(Font).MeasureString(Text);
            UI.DrawText(Text, new Vector2(position.Location.X + (position.Size.Width / 2) - (size.X / 2), position.Location.Y + (position.Size.Height / 2) - (size.Y / 2)), TextColor, Font);
        }
    }
}
