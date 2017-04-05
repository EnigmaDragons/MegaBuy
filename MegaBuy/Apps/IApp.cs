using MonoDragons.Core.Engine;

namespace MegaBuy.Apps
{
    public interface IApp : IVisualAutomaton
    {
        App Type { get; }
    }
}
