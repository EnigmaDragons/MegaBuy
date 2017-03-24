using Microsoft.Xna.Framework;

namespace MonoDragons.Core.UserInterface
{
    public abstract class ClickableUIElement
    {
        public abstract void OnEntered();
        public abstract void OnExitted();
        public abstract void OnPressed();
        public abstract void OnReleased();

        public int Layer { get; }
        public Rectangle Area { get; }

        protected ClickableUIElement(int layer, Rectangle area)
        {
            Layer = layer;
            Area = area;
        }
    }
}
