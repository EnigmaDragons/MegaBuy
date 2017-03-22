using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.PhysicsEngine
{
    // TODO: Build this Render Component
    public class Render : IVisual
    {
        private readonly string _sprite;
        private readonly Transform _location;

        public Render(string sprite, Transform location)
        {
            _sprite = sprite;
            _location = location;
        }

        public void Draw(Transform parentTransform)
        {
            throw new NotImplementedException();
        }
    }
}
