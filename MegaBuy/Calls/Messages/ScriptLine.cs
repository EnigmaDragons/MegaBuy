namespace MegaBuy.Calls.Messages
{
    public sealed class ScriptLine
    {
        public CallRole Role { get; }
        public string Text { get; }

        public ScriptLine(CallRole callRole, string text)
        {
            Role = callRole;
            Text = text;
        }
    }
}
