using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ImageBox : IVisual
    {
        private string _image;

        public Transform2 Transform { get; }

        public ImageBox(Transform2 transform) : this(transform, "none") {}

        public ImageBox(Transform2 transform, string name)
        {
            Transform = transform;
            _image = name;
        }

        public void SetImage(string name)
        {
            _image = name;
        }

        public void Clear()
        {
            _image = "none";
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!"none".Equals(_image))
                World.Draw(_image, parentTransform + Transform);
        }
    }
}
