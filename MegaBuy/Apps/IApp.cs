using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    public interface IApp : IVisualAutomaton
    {
        App Type { get; }
        ClickUIBranch Branch { get; }
    }
}
