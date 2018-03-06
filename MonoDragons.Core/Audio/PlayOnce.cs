using NAudio.Wave;
using System;

namespace MonoDragons.Core.Audio
{
    internal sealed class PlayOnce : ISampleProvider
    {
        private readonly AudioFileReader _reader;
        private bool _isDisposed;

        public event EventHandler OnSoundFinished;

        public WaveFormat WaveFormat => _reader.WaveFormat;

        public PlayOnce(string fileName)
            : this(new AudioFileReader(fileName))
        {
        }

        public PlayOnce(AudioFileReader reader)
        {
            _reader = reader;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (_isDisposed)
                return 0;

            var read = _reader.Read(buffer, offset, count);
            if (read < count)
            {
                _reader.Dispose();
                _isDisposed = true;

                OnSoundFinished?.Invoke(this, EventArgs.Empty);
            }

            return read;
        }
    }
}
