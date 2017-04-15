using MegaBuy.Money;

namespace MegaBuy.Food
{
    public class Food
    {
        public string Name { get; }
        public IAmount Cost { get; }
        public HungerRecovery Recovery { get; }
        public string Image { get; }

        public Food(string name, IAmount cost, int hungerRecovery, string image)
            : this(name, cost, new HungerRecovery(hungerRecovery), image) { }

        public Food(string name, IAmount cost, HungerRecovery recovery, string image)
        {
            Name = name;
            Cost = cost;
            Recovery = recovery;
            Image = image;
        }
    }
}
