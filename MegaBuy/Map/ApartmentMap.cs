using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Player;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Map
{
    public class ApartmentMap : IVisualAutomaton, ICharSpace
    {
        private readonly List<Tile> _tiles = new List<Tile>();

        public void Add(Tile tile)
        {
            _tiles.Add(tile);
        }

        public void Add(IEnumerable<Tile> tiles)
        {
            _tiles.AddRange(tiles);
        }

        public bool Exist(TileLocation loc)
        {
            return _tiles.Any(x => x.Location.Equals(loc));
        }

        public Tile Get(TileLocation loc)
        {
            return _tiles.First(x => x.Location.Equals(loc));
        }

        public void Update(TimeSpan delta)
        {
            _tiles.OrderBy(x => x.Layer).ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _tiles.OrderBy(x => x.Layer).ForEach(x => x.Draw(parentTransform));
        }

        public Transform2 ApplyMove(Transform2 transform, BoxCollider collider, Vector2 moveBy)
        {
            var proposedLocation = new Rectangle(collider.Rectangle.Location + moveBy.ToPoint(), collider.Rectangle.Size);
            if (_tiles.Where(x => x.IsBlocking).Any(x => proposedLocation.Intersects(x.Collider.Rectangle)))
                return transform;
            return transform + moveBy;
        }

        public void Interact(Transform2 transform)
        {
            var playerTile = new TileLocation(transform);
            var offset = transform.Rotation.ToDirection().AsOffset();
            var targetLocation = playerTile.Plus(new TileLocation(offset.Y, offset.X));
            Get(targetLocation).Interact();
        }
    }
}
