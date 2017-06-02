using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Apartment.Map;
using MegaBuy.Player;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MegaBuy.UIs;

namespace MegaBuy.Apartment
{
    public class ApartmentMap : IVisualAutomaton, ICharSpace
    {
        private readonly List<ClickableTile> _tiles = new List<ClickableTile>();

        public ClickUIBranch Branch { get; private set; } = new ClickUIBranch("room", (int)ClickUIPriorities.Room);

        public void Add(ClickableTile tile)
        {
            _tiles.Add(tile);
            Branch.Add(tile);
        }

        public void Add(IEnumerable<ClickableTile> tiles)
        {
            _tiles.AddRange(tiles);
            tiles.ForEach((t) => Branch.Add(t));
        }

        public bool Exist(TileLocation loc)
        {
            return _tiles.Any(x => x.Location.Equals(loc));
        }

        public List<ClickableTile> Get(TileLocation loc)
        {
            return _tiles.Where(x => x.Location.Equals(loc)).ToList();
        }

        public void Update(TimeSpan delta)
        {
            _tiles.OrderBy(x => x.Layer).ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            Branch.Scale = parentTransform.Scale;
            _tiles.OrderBy(x => x.Layer).ForEach(x => x.Draw(parentTransform));
        }

        public Transform2 ApplyMove(Transform2 transform, BoxCollider collider, Vector2 moveBy)
        {
            if (moveBy.Equals(Vector2.Zero))
                return transform;

            var proposedLocation = collider.Transform + moveBy;
            if (_tiles.Where(x => x.IsBlocking).Any(x => proposedLocation.Intersects(x.Collider.Transform)))
                return transform;
            return transform + moveBy;
        }

        // @todo #1 Make the interact detection feel a little bit nicer... bigger range, etc.
        public void Interact(TileLocation location)
        {
            Get(location).ForEach(x => x.Interact());
        }
    }
}
