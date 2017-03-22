using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Common
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            collection.ToList().ForEach(action);
        }
    }
}
