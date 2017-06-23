using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public sealed class GameObjects : IEntities
    {
        private readonly List<GameObject> _entities = new List<GameObject>();

        private int _nextId;

        public int Count => _entities.Count;

        public GameObject Create(Transform2 transform)
        {
            var obj = new GameObject(_nextId++, transform);
            _entities.Add(obj);
            return obj;
        }

        public void ForEach(Action<GameObject> action)
        {
            _entities.ToList().ForEach(action);
        }

        public void Remove(GameObject gameObject)
        {
            _entities.Remove(gameObject);
        }
    }
}
