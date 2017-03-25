using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy
{
    static class IEnumableExtensions
    {
        public static bool ContainsTypeOf<T>(this IEnumerable<T> items, Type type)
        {
            foreach (var item in items)
                if (item.GetType() == type)
                    return true;
            return false;
        }
    }
}
