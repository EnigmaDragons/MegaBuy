using MegaBuy.Money.Amounts;
using MegaBuy.Player.Hungers;

namespace MegaBuy.Foods
{
    public class Food
    {
        public string Name { get; }
        public IAmount Cost { get; }
        public HungerRecovery Recovery { get; }

        public Food(string name, IAmount cost, int hungerRecovery)
            : this(name, cost, new HungerRecovery(hungerRecovery)) { }

        public Food(string name, IAmount cost, HungerRecovery recovery)
        {
            Name = name;
            Cost = cost;
            Recovery = recovery;
        }
    }
}
