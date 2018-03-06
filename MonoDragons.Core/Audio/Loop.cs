using NAudio.Wave;

namespace MonoDragons.Core.Audio
{
    internal sealed class Looping : ISampleProvider
    {
        private readonly AudioFileReader _reader;

        public WaveFormat WaveFormat => _reader.WaveFormat;

        public Looping(string fileName)
            : this(new AudioFileReader(fileName))
        {
        }

        public Looping(AudioFileReader reader)
        {
            _reader = reader;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var read = _reader.Read(buffer, offset, count);
            if (read < count)
            {
                _reader.Position = 0;
                read = _reader.Read(buffer, offset, count);
            }
            return read;
        }
    }
}
