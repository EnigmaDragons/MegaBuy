namespace MegaBuy.Calls.Messages
{
    public sealed class ChatMessage
    {
        public CallRole Role { get; }
        public string Text { get; }

        public ChatMessage(CallRole callRole, string text)
        {
            Role = callRole;
            Text = text;
        }
    }
}
