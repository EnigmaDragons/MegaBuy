
namespace MegaBuy.Time
{
    public sealed class TimeRateChanged
    {
        public float Factor { get; }

        public TimeRateChanged(float factor)
        {
            Factor = factor;
        }
    }
}
