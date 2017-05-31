using MonoDragons.Core.Engine;

namespace MegaBuy.Pads.Apps
{
    public interface IApp : IVIsualAutomatonControl
    {
        App Type { get; }
    }
}
