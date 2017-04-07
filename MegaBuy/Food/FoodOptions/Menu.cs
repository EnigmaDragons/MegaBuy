using MonoDragons.Core.Engine;

namespace MegaBuy.Food.FoodOptions
{
    public static class Menu
    {
        private static Map<string, Food> _menu = new Map<string, Food>();

        public static Food Create(string food)
        {
            return new Food("None", new FoodCost(10), 10);
        }
    }
}
