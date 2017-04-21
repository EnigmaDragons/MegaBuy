using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;

namespace MonoDragons.Core.UserInterface
{
    public sealed class Label : IVisual, IDisposable
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
            _background.Draw(parentTransform);
            UI.DrawTextCentered(Text, new Rectangle((parentTransform.Location + Transform.Location).ToPoint(), Transform.Size.ToPoint()), TextColor, Font);
        }

        public void Dispose()
        {
            _background.Dispose();
        }
    }
}
