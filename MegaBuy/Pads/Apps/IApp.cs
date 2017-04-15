using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Pads.Apps
{
    public interface IApp : IVisualAutomaton
    {
        App Type { get; }
        ClickUIBranch Branch { get; }
    }
}
