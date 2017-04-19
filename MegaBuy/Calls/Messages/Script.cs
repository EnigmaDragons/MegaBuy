using System.Collections.Generic;

namespace MegaBuy.Calls.Messages
{
    public sealed class Script : List<ScriptLine>
    {
        public void Add(CallRole role, string line)
        {
            Add(new ScriptLine(role, line));
        }

        public void CallerSays(string line)
        {
            Add(CallRole.Caller, line);
        }

        public void PlayerSays(string line)
        {
            Add(CallRole.Player, line);
        }
    }
}
