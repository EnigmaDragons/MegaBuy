namespace MegaBuy.Notifications
{
    public sealed class PlayerNotification
    {
        public string Time { get; }
        public string Sender { get; }
        public string Message { get; }

        public PlayerNotification(string sender, string message)
            : this (GameState.Clock.Time, sender, message) { }

        private PlayerNotification(string time, string sender, string message)
        {
            Time = time;
            Sender = sender;
            Message = message;
        }
    }
}