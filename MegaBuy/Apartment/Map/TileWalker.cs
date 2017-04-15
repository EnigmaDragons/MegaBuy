using System;
using System.Collections.Generic;
using System.Linq;

namespace MegaBuy.Apartment.Map
{
    public class TileWalker
    {
        private readonly int _xStart;
        private readonly int _xNum;
        private readonly int _yStart;
        private readonly int _yNum;

        public TileWalker(int xStart, int xNum, int yStart, int yNum)
        {
            _xStart = xStart;
            _xNum = xNum;
            _yStart = yStart;
            _yNum = yNum;
        }

        private IEnumerable<TileLocation> GetTiles()
        {
            return Enumerable.Range(_xStart, _xNum)
                .SelectMany(x => Enumerable.Range(_yStart, _yNum)
                    .Select(y => new TileLocation(x, y)));
        }

        public IEnumerable<Tile> Get(Func<TileLocation, Tile> func)
        {
            return GetTiles().Select(func);
        }
    }
}
