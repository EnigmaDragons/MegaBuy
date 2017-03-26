using System;

namespace MonoDragons.Core.Engine
{
    public sealed class Timer : IAutomaton
    {
        private readonly Action _task;
        private readonly int _intervalMs;

        private double _elapsedMs;

        public Timer(Action task, int intervalMs)
        {
            _task = task;
            _intervalMs = intervalMs;
        }

        public void Update(TimeSpan delta)
        {
            _elapsedMs += delta.TotalMilliseconds;
            if (!(_elapsedMs > _intervalMs)) return;

            _task();
            _elapsedMs -= _intervalMs;
        }
    }
}
