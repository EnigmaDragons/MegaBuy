using MegaBuy.UIs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Jobs.Referrer
{
    public class ReferrerUI : IVisualControl
    {
        public ClickUIBranch Branch { get; } = new ClickUIBranch("Referrer", (int)ClickUIPriorities.Pad);

        public void Draw(Transform2 parentTransform)
        {
        }
    }
}
