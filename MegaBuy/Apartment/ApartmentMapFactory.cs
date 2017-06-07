using MegaBuy.Apartment.Map;
using MegaBuy.Player.Energy;
using MegaBuy.Player.Thoughts;
using MonoDragons.Core.Engine;

namespace MegaBuy.Apartment
{
    public static class ApartmentMapFactory
    {
        public static ApartmentMap Create()
        {
            const int width = 8;
            const int height = 8;

            var map = new ApartmentMap();
            map.Add(new TileWalker(1, width, 1, height).Get(x => new ClickableTile("2/floor", x, false, 0)));
            
            map.Add(new TileWalker(0, 1, 1, height - 1).Get(x => new ClickableTile("2/wall147", x, true, 1)));
            map.Add(new TileWalker(width, 1, 1, height - 1).Get(x => new ClickableTile("2/wall369", x, true, 1)));
            map.Add(new TileWalker(1, width - 1, 0, 1).Get(x => new ClickableTile("2/wall123top", x, true, 1)));
            map.Add(new TileWalker(1, width - 1, 1, 1).Get(x => new ClickableTile("2/wall123bot", x, true, 1)));
            map.Add(new TileWalker(1, width - 1, height, 1).Get(x => new ClickableTile("2/wall789", x, true, 1)));
            map.Add(new ClickableTile("2/wall124", new TileLocation(0, 0), true, 1));
            map.Add(new ClickableTile("2/wall236", new TileLocation(width, 0), true, 1));
            map.Add(new ClickableTile("2/wall478", new TileLocation(0, height), true, 1));
            map.Add(new ClickableTile("2/wall689", new TileLocation(width, height), true, 1));

            map.Add(new ClickableTile("2/bed-top", new TileLocation(1, 2), true, () => World.Publish(new PreparingForBed()), 1));
            map.Add(new ClickableTile("2/bed-bot", new TileLocation(1, 3), true, () => World.Publish(new PreparingForBed()), 1));
            
            map.Add(new ClickableTile("2/laundry2", new TileLocation(2, 4), false, () => World.Publish(new HadAThought(Laundry.GetThought())), 1));
            
            map.Add(new ClickableTile("2/door-top", new TileLocation(width - 1, 0), false, () => World.Publish(new HadAThought(Outside.GetThought())), 2));
            map.Add(new ClickableTile("2/door-bot", new TileLocation(width - 1, 1), false, () => World.Publish(new HadAThought(Outside.GetThought())), 2));
            map.Add(new ClickableTile("2/security1", new TileLocation(width - 2, 1), false, 2));

            map.Add(new ClickableTile("2/boxofstims", new TileLocation(1, height - 2), true, () => World.Publish(new HadAThought(BottleBox.GetThought())), 2));

            map.Add(new ClickableTile("2/tv1", new TileLocation(1, 0), false, 2));

            map.Add(new ClickableTile("2/shower-top", new TileLocation(width - 1, 5), true, () => World.Publish(new HadAThought(Shower.GetThought())), 2));
            map.Add(new ClickableTile("2/shower-bot", new TileLocation(width - 1, 6), true, () => World.Publish(new HadAThought(Shower.GetThought())), 2));
            map.Add(new ClickableTile("2/sink-right", new TileLocation(width - 1, 7), true, 2));

            map.Add(new ClickableTile("2/poster1-top", new TileLocation(4, 0), false, 2));
            map.Add(new ClickableTile("2/poster1-bot", new TileLocation(4, 1), false, 2));

            map.Add(new ClickableTile("2/desk1-1", new TileLocation(width / 2 - 1, height - 3), true, () => World.Publish(new HadAThought(ComputerRig.GetThought())), 2));
            map.Add(new ClickableTile("2/desk1-2", new TileLocation(width / 2, height - 3), true, () => World.Publish(new HadAThought(ComputerRig.GetThought())), 2));
            map.Add(new ClickableTile("2/desk1-3", new TileLocation(width / 2 - 1, height - 2), true, () => World.Publish(new HadAThought(ComputerRig.GetThought())), 2));
            map.Add(new ClickableTile("2/desk1-4", new TileLocation(width / 2, height - 2), true, () => World.Publish(new HadAThought(ComputerRig.GetThought())), 2));

            var counter = new FoodCounter(new TileLocation(width - 1, 3));
            map.Add(counter.Tile);
            map.Add(counter.FoodTile);

            return map;
        }
    }
}
