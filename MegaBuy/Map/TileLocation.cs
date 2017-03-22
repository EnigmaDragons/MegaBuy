using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Map
{
    public class TileLocation
    {
        public int Column { get; }
        public int Row { get; }
        public Transform Transform => new Transform(Position, Rotation.Default, 1);
        public Vector2 Position => new Vector2(Column * TileSize.Int, Row * TileSize.Int);
        
        public TileLocation(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public List<TileLocation> Through(TileLocation end)
        {
            var locs = new List<TileLocation>();
            var xCondition = Column <= end.Column;
            var yCondition = Row <= end.Row;
            for (var x = Column; xCondition ? x < end.Column + 1 : x > end.Column - 1; x = xCondition ? x + 1 : x - 1)
                for (var y = Row; yCondition ? y < end.Row + 1 : y > end.Row - 1; y = yCondition ? y + 1 : y - 1)
                    locs.Add(new TileLocation(x, y));
            return locs;
        }
        
        protected bool Equals(TileLocation other)
        {
            return Column == other.Column && Row == other.Row;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TileLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Column * 397) ^ Row;
            }
        }

        public TileLocation Plus(int x, int y)
        {
            return new TileLocation(Column + x, Row + y);
        }

        public TileLocation Plus(TileLocation loc)
        {
            return Plus(loc.Row, loc.Column);
        }

        public override string ToString()
        {
            return $"{Column}, {Row}";
        }
    }
}
