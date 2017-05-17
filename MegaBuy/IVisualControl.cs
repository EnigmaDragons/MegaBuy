using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy
{
    public interface IVisualControl : IVisual
    {
        ClickUIBranch Branch { get; }
    }
}
