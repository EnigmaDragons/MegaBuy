using System.Collections.Generic;

namespace MegaBuy.Calls.Callers
{
    public class Traits : Dictionary<string, string>
    {
        private Traits(Dictionary<string, string> dict) : base(dict)
        {
        }

        public static Traits None = new Traits(new Dictionary<string, string>());

        public static Traits Create(params KeyValuePair<string, object>[] setTraits)
        {
            var traits = None;
            foreach (var pair in setTraits)
                traits[pair.Key] = pair.Value.ToString();
            return traits;
        }
    }
}
