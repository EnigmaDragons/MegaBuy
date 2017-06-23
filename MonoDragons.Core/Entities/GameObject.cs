
using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public sealed class GameObject
    {
        private readonly Map<Type, object> _components = new Map<Type, object>();

        public int Id { get; }
        public Transform2 Transform { get; }

        internal GameObject(int id, Transform2 transform)
        {
            Id = id;
            Transform = transform;
        }

        public override bool Equals(object obj)
        {
            return obj is GameObject && obj.GetHashCode().Equals(GetHashCode());
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public GameObject Add<T>(T component)
        {
            return Add(component, typeof(T));
        }

        public GameObject Add(object component)
        {
            return Add(component, component.GetType());
        }

        public GameObject Add(object component, Type componentType)
        {
            if (_components.ContainsKey(componentType))
                throw new InvalidOperationException($"Cannot add more than one {componentType.Name} component.");
            _components.Add(componentType, component);
            return this;
        }
        
        public GameObject Add(Func<GameObject, object> componentBuilder)
        {
            Add(componentBuilder(this));
            return this;
        }

        public T Get<T>()
        {
            return (T)_components[typeof(T)];
        }

        public void With<T>(Action<T> action)
        {
            var type = typeof(T);
            if (_components.ContainsKey(type))
                action((T)_components[type]);
        }
    }
}
