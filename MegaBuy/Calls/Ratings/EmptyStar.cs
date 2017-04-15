using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Calls.Ratings
{
    public class EmptyStar : IVisual
    {
        private const string _name = "Images/PAD/star-empty";
        private readonly Size2 _size = new Size2(150, 150);
        private readonly Vector2 _placement;

        public EmptyStar(Vector2 placement)
        {
            _placement = placement;
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(_name, new Transform2(_placement, _size));
        }
    }
}
