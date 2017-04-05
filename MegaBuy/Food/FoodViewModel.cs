using MegaBuy.Money;

namespace MegaBuy.Food
{
    public class FoodViewModel
    {
        public string Name { get; private set; }
        public IAmount Cost { get; private set; }
        public string Image { get; private set; }

        public FoodViewModel(string name, IAmount cost, string image)
        {
            Name = name;
            Cost = cost;
            Image = image;
        }
    }
}
