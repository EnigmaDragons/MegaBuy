using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace EncryptionLayer.Player
{
    public interface ICharSpace
    {
        Transform ApplyMove(Transform transform, BoxCollider collider, Vector2 moveBy);
    }
}
