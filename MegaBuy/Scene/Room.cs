using System;
using MegaBuy.Map;
using MegaBuy.Player;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scene
{
    public class Room : IScene
    {
        private ApartmentMap _map;
        private PlayerCharacter _player;

        private readonly Transform2 _camera = new Transform2(new Vector2(180, 0), Rotation2.Default, 2);

        public void Init()
        {
            _map = new ApartmentMap();
            _map.Add(new TileWalker(1, 13, 1, 13).Get(x => new Tile("floor", x, false, 0)));

            _map.Add(new Tile("bed-top", new TileLocation(12, 5), true, 1));
            _map.Add(new Tile("bed-mid", new TileLocation(12, 6), true, 1));
            _map.Add(new Tile("bed-bot", new TileLocation(12, 7), true, 1));

            _map.Add(new TileWalker(0, 1, 1, 12).Get(x => new Tile("wall-left", x, true, 1)));
            _map.Add(new TileWalker(13, 1, 1, 12).Get(x => new Tile("wall-right", x, true, 1)));
            _map.Add(new TileWalker(1, 12, 0, 1).Get(x => new Tile("wall-top", x, true, 1)));
            _map.Add(new TileWalker(1, 12, 13, 1).Get(x => new Tile("wall-bot", x, true, 1)));
            _map.Add(new Tile("wall-top-left", new TileLocation(0, 0), true, 1));
            _map.Add(new Tile("wall-top-right", new TileLocation(13, 0), true, 1));
            _map.Add(new Tile("wall-bot-left", new TileLocation(0, 13), true, 1));
            _map.Add(new Tile("wall-bot-right", new TileLocation(13, 13), true, 1));

            _map.Add(new Tile("desk1", new TileLocation(2, 11), true, 1));
            _map.Add(new Tile("desk2", new TileLocation(3, 11), true, 1));
            _map.Add(new Tile("desk3", new TileLocation(4, 11), true, 1));
            _map.Add(new Tile("desk4", new TileLocation(2, 12), true, 1));
            _map.Add(new Tile("desk5", new TileLocation(3, 12), true, 1));
            _map.Add(new Tile("desk6", new TileLocation(4, 12), true, 1));

            _player = new PlayerCharacter(_map, new TileLocation(11, 6).Transform);
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
            World.Draw("Effects/light-effect", new Rectangle(350, 0, 900, 900));
        }
    }
}
