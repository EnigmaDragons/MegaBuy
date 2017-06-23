using System;

namespace MonoDragons.Core.Scenes
{
    public interface IScene
    {
        void Init();
        void Update(TimeSpan delta);
        void Draw();
    }
}
