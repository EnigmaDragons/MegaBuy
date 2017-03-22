using System;
using MegaBuy.Map;
using MegaBuy.Player;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scenes
{
    public class Room : IScene
    {
        private ApartmentMap _map;
        private PlayerCharacter _player;

        private readonly Transform _camera = new Transform(new Vector2(-16, 0), Rotation.Default, 2);

        public void Init()
        {
            _map = new ApartmentMap();
            _map.Add(new TileWalker(1, 24, 1, 13).Get(x => new Tile("floor", x, false, 0)));

            _map.Add(new Tile("bed-top", new TileLocation(24, 5), true, 1));
            _map.Add(new Tile("bed-mid", new TileLocation(24, 6), true, 1));
            _map.Add(new Tile("bed-bot", new TileLocation(24, 7), true, 1));

            _map.Add(new TileWalker(0, 1, 1, 12).Get(x => new Tile("wall-left", x, true, 1)));
            _map.Add(new TileWalker(25, 1, 1, 12).Get(x => new Tile("wall-right", x, true, 1)));
            _map.Add(new TileWalker(1, 24, 0, 1).Get(x => new Tile("wall-top", x, true, 1)));
            _map.Add(new TileWalker(1, 24, 13, 1).Get(x => new Tile("wall-bot", x, true, 1)));
            _map.Add(new Tile("wall-top-left", new TileLocation(0, 0), true, 1));
            _map.Add(new Tile("wall-top-right", new TileLocation(25, 0), true, 1));
            _map.Add(new Tile("wall-bot-left", new TileLocation(0, 13), true, 1));
            _map.Add(new Tile("wall-bot-right", new TileLocation(25, 13), true, 1));

            _player = new PlayerCharacter(_map, new TileLocation(8, 9).Transform);
        }

        public void Update(TimeSpan delta)
        {
            _player.Update(delta);
            _map.Update(delta);
        }

        public void Draw()
        { 
            _map.Draw(_camera);
            _player.Draw(_camera);
        }
    }
}
