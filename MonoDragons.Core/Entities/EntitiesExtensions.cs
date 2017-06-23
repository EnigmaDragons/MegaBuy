using System;

namespace MonoDragons.Core.Entities
{
    public static class EntitiesExtensions
    {
        public static void ForEach<T>(this IEntities entities, Action<T> action)
        {
            entities.ForEach(e => e.With(action));
        }
    }
}
