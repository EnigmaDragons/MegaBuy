using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MonoDragons.Core.Audio
{
    internal class AudioPlayer : IDisposable
    {
        public static readonly AudioPlayer Instance = new AudioPlayer(44100, 2);

        private readonly IWavePlayer _player;
        private readonly MixingSampleProvider _mixer;

        public AudioPlayer(int sampleRate = 44100, int channelCount = 2)
        {
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            _mixer.ReadFully = true;
            _player = new WaveOutEvent();
            _player.Init(_mixer);
            _player.Play();
        }

        public void Play(ISampleProvider samples)
        {
            AddMixerInput(samples);
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == _mixer.WaveFormat.Channels)
                return input;
            if (input.WaveFormat.Channels == 1 && _mixer.WaveFormat.Channels == 2)
                return new MonoToStereoSampleProvider(input);
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        private void AddMixerInput(ISampleProvider input)
        {
            _mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        public void Dispose()
        {
            _player.Dispose();
        }
    }
}
