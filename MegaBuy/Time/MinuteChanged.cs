namespace MegaBuy.Time
{
    public struct MinuteChanged
    {
        public int Hour { get; }
        public int Minute { get; }

        public MinuteChanged(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
    }
}