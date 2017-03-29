using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class CallerNames
    {
        public static string[] names = new string[] { "Bob, John, Jane, Mary" };
        public static string GetRandomName()
        {
            return names[Rng.Int(CallerNames.names.Length)];
        }
    }
}
