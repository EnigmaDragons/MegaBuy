﻿
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Inputs
{
    public struct Direction
    {
        public static Direction None = new Direction(HorizontalDirection.None, VerticalDirection.None);

        public HorizontalDirection HDir { get; }
        public VerticalDirection VDir { get; }
        
        public Direction(HorizontalDirection hDir, VerticalDirection vDir)
        {
            HDir = hDir;
            VDir = vDir;
        }

        public Point AsOffset()
        {
            return new Point((int)HDir, (int)VDir);
        }
    }
}
