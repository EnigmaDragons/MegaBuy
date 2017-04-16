
using MegaBuy.Apartment.Map;
using MegaBuy.Player.Energy;
using MonoDragons.Core.Engine;

namespace MegaBuy.Apartment
{
    public static class ApartmentMapFactory
    {
        public static ApartmentMap Create()
        {
            var map = new ApartmentMap();
            map.Add(new TileWalker(1, 13, 1, 13).Get(x => new Tile("floor", x, false, 0)));

            map.Add(new Tile("bed-top", new TileLocation(12, 5), true, () => World.Publish(new PreparingForBed()), 1));
            map.Add(new Tile("bed-mid", new TileLocation(12, 6), true, () => World.Publish(new PreparingForBed()), 1));
            map.Add(new Tile("bed-bot", new TileLocation(12, 7), true, () => World.Publish(new PreparingForBed()), 1));

            map.Add(new TileWalker(0, 1, 1, 12).Get(x => new Tile("wall-left", x, true, 1)));
            map.Add(new TileWalker(13, 1, 1, 12).Get(x => new Tile("wall-right", x, true, 1)));
            map.Add(new TileWalker(1, 12, 0, 1).Get(x => new Tile("wall-top", x, true, 1)));
            map.Add(new TileWalker(1, 12, 13, 1).Get(x => new Tile("wall-bot", x, true, 1)));
            map.Add(new Tile("wall-top-left", new TileLocation(0, 0), true, 1));
            map.Add(new Tile("wall-top-right", new TileLocation(13, 0), true, 1));
            map.Add(new Tile("wall-bot-left", new TileLocation(0, 13), true, 1));
            map.Add(new Tile("wall-bot-right", new TileLocation(13, 13), true, 1));
            return map;
        }
    }
}
