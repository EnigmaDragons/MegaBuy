using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Map
{
    public class Tile
    {
        public string TextureName { get; }
        public TileLocation Location { get; }
        public Transform2 Transform { get; }
        public bool IsBlocking { get; }
        public int Layer { get; }
        public BoxCollider Collider { get; } 
        public Action Interact { get; }
        
        public Tile(string textureName, TileLocation location, bool blocking, int layer = 0)
            : this (textureName, location, blocking, () => { }, layer) { }

        public Tile(string textureName, TileLocation location, bool blocking, Action onInteract, int layer = 0)
        {
            TextureName = "Images/Map/" + textureName;
            Location = location;
            Transform = location.Transform;
            IsBlocking = blocking;
            Layer = layer;
            Interact = onInteract;
            Collider = new BoxCollider(Transform, new Point(TileSize.Int, TileSize.Int));
        }

        public virtual void Update(TimeSpan delta)
        {
        }

        public virtual void Draw(Transform2 parentTransform)
        {
            World.Draw(TextureName, parentTransform + Transform);
        }
    }
}
