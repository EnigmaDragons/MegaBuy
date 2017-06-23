using System;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Scenes
{
    public sealed class SceneAutomatons : IDisposable
    {
        private readonly Map<IAutomaton, GameObject> _objs = new Map<IAutomaton, GameObject>();

        public void Add(IAutomaton automaton)
        {
            var obj = Entity.Create(Transform2.Zero).Add(automaton);
            _objs[automaton] = obj;
        }

        public void Remove(IAutomaton automaton)
        {
            if (!_objs.ContainsKey(automaton))
                return;

            var obj = _objs[automaton];
            Entity.Destroy(obj);
            _objs.Remove(automaton);
        }

        public void Dispose()
        {
            _objs.Keys.ForEach(Remove);
        }
    }
}
