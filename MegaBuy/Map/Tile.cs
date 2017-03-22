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
        public bool IsBlocking { get; }
        public int Layer { get; }
        public BoxCollider Collider { get; } 

        public Tile(string textureName, TileLocation loc, bool blocking, int layer = 0)
        {
            TextureName = "Images/Map/" + textureName;
            Location = loc;
            IsBlocking = blocking;
            Layer = layer;
            Collider = new BoxCollider(new Rectangle(loc.RenderPosition.ToPoint(), new Point(TileSize.RenderSize, TileSize.RenderSize)));
        }

        public virtual void Update(TimeSpan delta)
        {
        }

        public virtual void Draw(Transform parentTransform)
        {
            var rectangle = new Rectangle((int)(Location.RenderPosition.X + parentTransform.Location.X), (int)(Location.RenderPosition.Y + parentTransform.Location.Y), TileSize.RenderSize, TileSize.RenderSize);
            World.Draw(TextureName, rectangle);
        }
    }
}
