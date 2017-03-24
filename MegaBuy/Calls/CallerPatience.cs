
namespace MegaBuy.Calls
{
    public struct CallerPatience
    {
        public int Value { get; private set; }

        public CallerPatience(int initial)
        {
            Value = initial;
        }

        public void Reduce()
        {
            Value--;
        }
    }
}
