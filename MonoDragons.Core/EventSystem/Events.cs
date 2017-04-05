using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.EventSystem
{
    public class Events
    {
        private readonly Dictionary<Type, List<object>> _events = new Dictionary<Type, List<object>>();
        private readonly Dictionary<object, List<object>> _owners = new Dictionary<object, List<object>>();

        public int SubscriptionCount => _events.Sum(e => e.Value.Count);

        public void Publish<T>(T payload)
        {
            var eventType = typeof(T);
            if (!_events.ContainsKey(eventType))
                return;
            foreach (var e in _events[eventType].ToList())
                ((Action<T>)e)(payload);
        }

        public void Subscribe<T>(EventSubscription<T> subscription)
        {
            var eventType = typeof(T);
            if (!_events.ContainsKey(eventType))
                _events[eventType] = new List<object>();
            if (!_owners.ContainsKey(subscription.Owner))
                _owners[subscription.Owner] = new List<object>();
            _events[eventType].Add(subscription.OnEvent);
            _owners[subscription.Owner].Add(subscription.OnEvent);
        }

        public void Unsubscribe(object owner)
        {
            if (!_owners.ContainsKey(owner))
                return;
            var events = _owners[owner];
            for (var i = 0; i < _events.Count; i++)
                _events.ElementAt(i).Value.RemoveAll(x => events.Any(y => y.Equals(x)));
            _owners.Remove(owner);
        }
    }
}
