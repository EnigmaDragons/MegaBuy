using System;
using System.Collections.Generic;
using MonoDragons.Core.Scenes;

namespace MonoDragons.Core.Scenes
{
    public class SceneFactory
    {
        private readonly Dictionary<string, Func<IScene>> _sceneInstructions;

        public SceneFactory(Dictionary<string, Func<IScene>> sceneInstructions)
        {
            _sceneInstructions = sceneInstructions;
        }

        public IScene Create(string sceneName)
        {
            return _sceneInstructions[sceneName]();
        }
    }
}
