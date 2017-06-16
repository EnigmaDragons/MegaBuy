using MonoDragons.Core.Engine;

namespace MegaBuy.Pads.Apps
{
    public interface IApp : IVisualAutomatonControl
    {
        App Type { get; }
    }
}
