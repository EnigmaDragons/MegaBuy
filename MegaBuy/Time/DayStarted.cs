
namespace MegaBuy.Time
{
    public sealed class DayStarted
    {
        public int DayNumber { get; }

        public DayStarted(int dayNumber)
        {
            DayNumber = dayNumber;
        }
    }
}
