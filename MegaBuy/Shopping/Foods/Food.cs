using MegaBuy.Money.Amounts;
using MegaBuy.Player.Hungers;

namespace MegaBuy.Shopping.Foods
{
    public class Food : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public IAmount Cost { get; }
        public HungerRecovery HungerRecovery { get; }

        public Food(string name, ItemCost cost, int hungerRecovery, string description)
            : this(name, cost, new HungerRecovery(hungerRecovery), description) { }

        public Food(string name, ItemCost cost, HungerRecovery recovery, string description)
        {
            Name = name;
            Description = description;
            Cost = cost;
            HungerRecovery = recovery;
        }
    }
}
