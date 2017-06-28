using System.Collections.Generic;

namespace MegaBuy.Shopping.Upgrades
{
    public class FurnitureStore : IShoppingCompany
    {
        public string Name => "Furniture Store";
        public string Description => "World's most trusted, best respected, and only, furniture store";
        public List<IItem> Items => new List<IItem>();

        public void Buy(IItem item)
        {
        }
    }
}
