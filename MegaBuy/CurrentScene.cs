using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;

namespace MegaBuy
{
    public static class CurrentScene
    {
        private static SceneAutomatons Automatons { get; set; }

        public static SceneAutomatons BeginNew()
        {
            Automatons?.Dispose();
            Automatons = new SceneAutomatons();
            return Automatons;
        }

        public static void Add(IAutomaton automaton)
        {
            Automatons.Add(automaton);
        }

        public static void Remove(IAutomaton automaton)
        {
            Automatons.Remove(automaton);
        }
    }
}
