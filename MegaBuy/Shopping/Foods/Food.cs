using MegaBuy.Money.Amounts;
using MegaBuy.Player.Energy;
using MegaBuy.Player.Hungers;

namespace MegaBuy.Shopping.Foods
{
    public sealed class Food : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public IAmount Cost { get; }
        public HungerRecovery HungerRecovery { get; }
        public EnergyRecovery EnergyRecovery { get; }

        public Food(string name, ItemCost cost, int hungerRecovery, int energyRecovery, string description)
            : this(name, cost, new HungerRecovery(hungerRecovery), new EnergyRecovery(energyRecovery), description) { }

        public Food(string name, ItemCost cost, HungerRecovery hunger, EnergyRecovery energy, string description)
        {
            Name = name;
            Description = description;
            Cost = cost;
            HungerRecovery = hunger;
            EnergyRecovery = energy;
        }
    }
}
