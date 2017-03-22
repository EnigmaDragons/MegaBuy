using System;
using EncryptionLayer.Player;
using MegaBuy.Map;
using MegaBuy.Player;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scene
{
    public sealed class Darkness : IScene
    {
        private ApartmentMap _map;
        private PlayerCharacter _player;

        public void Init()
        {
            _map = new ApartmentMap();
            _player = new PlayerCharacter(_map, Transform.Zero);
        }

        public void Update(TimeSpan delta)
        {
            _player.Update(delta);
            _map.Update(delta);
        }

        public void Draw()
        {
            _player.Draw(new Transform(4));
            _map.Draw(new Transform(4));
        }
    }
}
