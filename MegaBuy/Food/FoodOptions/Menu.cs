using MonoDragons.Core.Engine;

namespace MegaBuy.Food.FoodOptions
{
    public static class Menu
    {
        public static Map<string, Food> FoodMenu = new Map<string, Food>
        {
            { "Burger", new Food("Value Burger", new FoodCost(5), 25, "nothing") }
        };

        public static Food Create(string food)
        {
            if (FoodMenu.ContainsKey(food))
                return FoodMenu[food];
            return new Food("None", new FoodCost(10), 10, "nothing");
        }
    }
}
