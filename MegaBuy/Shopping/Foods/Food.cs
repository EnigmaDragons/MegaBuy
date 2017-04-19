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

        public Food(string name, string description, ItemCost cost, int hungerRecovery)
            : this(name, description, cost, new HungerRecovery(hungerRecovery)) { }

        public Food(string name, string description, ItemCost cost, HungerRecovery recovery)
        {
            Name = name;
            Description = description;
            Cost = cost;
            HungerRecovery = recovery;
        }
    }
}
