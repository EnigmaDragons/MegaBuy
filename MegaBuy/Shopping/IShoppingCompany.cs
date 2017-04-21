using System.Collections.Generic;

namespace MegaBuy.Shopping
{
    public interface IShoppingCompany
    {
        string Name { get; }
        string Description { get; }
        List<IItem> Items { get; }
        void Buy(IItem item);
    }
}
