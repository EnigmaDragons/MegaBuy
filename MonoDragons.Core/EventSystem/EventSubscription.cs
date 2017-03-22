using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.EventSystem
{
    public class EventSubscription<T> : IDisposable
    {
        public Action<T> OnEvent { get; }
        public object Owner { get; }

        public EventSubscription(Action<T> onEvent, object owner)
        {
            OnEvent = onEvent;
            Owner = owner;
        }

        public void Dispose()
        {
            World.Unsubscribe(Owner);
        }
    }
}
