﻿using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.EventSystem
{
    public sealed class EventSubscription : IDisposable
    {
        public Type EventType { get; }
        public object OnEvent { get; }
        public object Owner { get; }

        private EventSubscription(Type eventType, object onEvent, object owner)
        {
            EventType = eventType;
            OnEvent = onEvent;
            Owner = owner;
        }

        public void Dispose()
        {
            World.Unsubscribe(Owner);
        }

        public static EventSubscription Create<T>(Action<T> onEvent, object owner)
        {
            return new EventSubscription(typeof(T), onEvent, owner);
        }
    }
}
