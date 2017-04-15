using MegaBuy.Apartment.Map;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Player
{
    public interface ICharSpace
    {
        void Interact(TileLocation location);
        Transform2 ApplyMove(Transform2 transform, BoxCollider collider, Vector2 moveBy);
    }
}
