using System;

namespace MegaBuy.Time
{
    public sealed class DayStarted
    {
        public DateTime Date { get; }

        public DayStarted(DateTime date)
        {
            Date = date;
        }
    }
}
