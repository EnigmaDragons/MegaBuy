using System;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Apps
{
    public sealed class NoneApp : IApp
    {
        public App Type => App.None;

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
        }

        public void LostFocus()
        {
        }

        public void GainedFocus()
        {
        }
    }
}
