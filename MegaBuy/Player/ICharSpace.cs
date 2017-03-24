using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace EncryptionLayer.Player
{
    public interface ICharSpace
    {
        Transform2 ApplyMove(Transform2 transform, BoxCollider collider, Vector2 moveBy);
    }
}
