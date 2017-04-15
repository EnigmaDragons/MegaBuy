using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Apartment.Map
{
    public static class TileSize
    {
        public static int Int => 32;

        public static Size2 Size => new Size2(Int, Int);
        public static Point Area => new Point(Int, Int);
    }
}
