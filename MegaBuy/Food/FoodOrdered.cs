namespace MegaBuy.Food
{
    public struct FoodOrdered
    {
        public Food Food { get; }

        public FoodOrdered(Food food)
        {
            Food = food;
        }
    }
}
