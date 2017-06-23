using System.Collections.Generic;

namespace MegaBuy.Calls.Messages
{
    public sealed class Chat : List<ChatMessage>
    {
        public Chat Add(CallRole role, string line)
        {
            Add(new ChatMessage(role, line));
            return this;
        }

        public Chat CallerSays(string line)
        {
            Add(CallRole.Caller, line);
            return this;
        }

        public Chat PlayerSays(string line)
        {
            Add(CallRole.Player, line);
            return this;
        }
    }
}
