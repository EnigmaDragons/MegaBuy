
namespace MegaBuy.Time
{
    public sealed class TimeRateChanged
    {
        public double MsPerMinute { get; }

        public TimeRateChanged(double msPerMinute)
        {
            MsPerMinute = msPerMinute;
        }
    }
}
