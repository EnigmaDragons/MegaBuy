using Microsoft.Xna.Framework;

namespace MonoDragons.Core.PhysicsEngine
{
    public struct BoxCollider
    {
        public Rectangle Rectangle { get; }

        public BoxCollider(Transform transform, Point size)
            : this(new Rectangle(transform.Location.ToPoint(), size)) { }

        public BoxCollider(Rectangle rect)
        {
            Rectangle = rect;
        }
    }
}
