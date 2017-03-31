using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class Label : IVisual
    {
        public string Font { get; set; } = "Fonts/Arial";
        public Transform2 Transform { get; set; } = new Transform2(new Size2(400, 100));
        public Color TextColor { get; set; } = Color.White;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public string Text { get; set; } = "";
      
        public void Draw(Transform2 parentTransform)
        {
            var position = parentTransform + Transform;
            World.DrawRectangle(position.ToRectangle(), BackgroundColor);
            var size = Resources.Load<SpriteFont>(Font).MeasureString(Text);
            UI.DrawText(Text, new Vector2(position.Location.X + (position.Size.Width / 2) - (size.X / 2), position.Location.Y + (position.Size.Height / 2) - (size.Y / 2)), TextColor, Font);
        }
    }
}
