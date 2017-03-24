using System;

namespace MonoDragons.Core.PhysicsEngine
{
    public struct Rotation2
    {
        public static Rotation2 None = new Rotation2(0);
        public static Rotation2 Default = new Rotation2(0);
        public static Rotation2 Up = new Rotation2(0);
        public static Rotation2 Right = new Rotation2((float)(Math.PI / 2));
        public static Rotation2 Down = new Rotation2((float)Math.PI);
        public static Rotation2 Left = new Rotation2((float)(Math.PI * 1.5));

        public float Value { get; }

        public Rotation2(float value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return Math.Abs(Value - ((Rotation2)obj).Value) < 0.01;
        }

        public override int GetHashCode()
        {
            return (int)Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static Rotation2 operator +(Rotation2 r1, Rotation2 r2)
        {
            var newValue = r1.Value + r2.Value;
            while (newValue >= 360)
                newValue -= 360;
            return new Rotation2(newValue);
        }
    }
}
