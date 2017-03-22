using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoDragons.Core.Engine
{
    public class SceneContents : IDisposable
    {
        private readonly List<IDisposable> _diposables = new List<IDisposable>();
        private readonly ContentManager _contentManager;

        public SceneContents(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void Put(IDisposable disposable)
        {
            _diposables.Add(disposable);
        }

        public T Load<T>(string resourceName) where T : IDisposable
        {
            return _contentManager.Load<T>(resourceName);
        }

        public void Dispose()
        {
            _diposables.ForEach(x => x.Dispose());
            _diposables.Clear();
            _contentManager.Unload();
        }
    }
}
