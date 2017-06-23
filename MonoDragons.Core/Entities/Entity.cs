using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public static class Entity
    {
        internal static readonly GameObjects Objs = new GameObjects();
        internal static readonly EntitySystem System = new EntitySystem(Objs);

        public static int Count => Objs.Count;

        public static void Register(ISystem system)
        {
            System.Register(system);
        }

        public static GameObject Create(Transform2 transform)
        {
            return Objs.Create(transform);
        }

        public static void Destroy(GameObject obj)
        {
            Objs.Remove(obj);
        }
    }
}
