using System;

namespace MonoDragons.Core.Common
{
    public static class Rng
    {
        private static readonly Random Instance = new Random(Guid.NewGuid().GetHashCode());

        public static int Int(int max)
        {
            return Instance.Next(max);
        }

        public static float Int(int min, int max)
        {
            return Instance.Next(min, max);
        }
    }
}
