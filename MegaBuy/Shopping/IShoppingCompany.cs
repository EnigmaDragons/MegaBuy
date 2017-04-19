using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Shopping
{
    public interface IShoppingCompany : IVisual
    {
        string Name { get; }
        string Description { get; }
        ClickUIBranch Branch { get; }
    }
}
