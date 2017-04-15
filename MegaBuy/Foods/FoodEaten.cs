namespace MegaBuy.Foods
{
    public sealed class FoodEaten
    {
        public Food Food { get; }

        public FoodEaten(Food food)
        {
            Food = food;
        }
    }
}
