using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MonoDragons.Core.Audio
{
    internal class AudioPlaybackEngine : IDisposable
    {
        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);

        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            ;
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice = new WaveOutEvent();
            outputDevice.Init(mixer);
            outputDevice.Play();
        }

        public void Play(ISampleProvider samples)
        {
            AddMixerInput(samples);
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
                return input;
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
                return new MonoToStereoSampleProvider(input);
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        private void AddMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }
    }
}
