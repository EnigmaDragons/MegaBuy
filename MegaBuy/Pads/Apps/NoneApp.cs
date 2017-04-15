using System;
using MegaBuy.Temp;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Pads.Apps
{
    public sealed class NoneApp : IApp
    {
        public App Type => App.None;
        public ClickUIBranch Branch { get; } = new ClickUIBranch("None", (int)ClickUIPriorities.Pad);

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
