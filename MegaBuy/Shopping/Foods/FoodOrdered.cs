namespace MegaBuy.Shopping.Foods
{
    public class FoodOrdered
    {
        public Food Food { get; }

        public FoodOrdered(Food food)
        {
            Food = food;
        }
    }
}