using Microsoft.Xna.Framework;

namespace MonoDragons.Core.PhysicsEngine
{
    public struct Transform
    {
        public static Transform Zero = new Transform(Vector2.Zero);

        public Vector2 Location { get; }
        public Rotation Rotation { get; }
        public float Scale { get; }

        public Transform(Vector2 location)
            : this(location, Rotation.Default, 1) { }

        public Transform(Vector2 location, Rotation rotation, float scale)
        {
            Location = location;
            Rotation = rotation;
            Scale = scale;
        }

        public static Transform operator+(Transform t1, Transform t2)
        {
            return new Transform(t1.Location + t2.Location, t1.Rotation, t1.Scale);
        }

        public static Transform operator +(Transform t1, Vector2 by)
        {
            return new Transform(t1.Location + by, t1.Rotation, t1.Scale);
        }
    }
}
