using System;

namespace MonoDragons.Core.PhysicsEngine
{
    public struct Rotation
    {
        public static Rotation None = new Rotation(0);
        public static Rotation Default = new Rotation(0);
        public static Rotation Up = new Rotation(0);
        public static Rotation Right = new Rotation((float)(Math.PI / 2));
        public static Rotation Down = new Rotation((float)Math.PI);
        public static Rotation Left = new Rotation((float)(Math.PI * 1.5));

        public float Value { get; }

        public Rotation(float value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return Math.Abs(Value - ((Rotation)obj).Value) < 0.01;
        }
    }
}
