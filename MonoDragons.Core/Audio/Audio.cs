using NAudio.Wave;
using System;

namespace MonoDragons.Core.Audio
{
    public static class Audio
    {
        private static Dampening _music;

        public static void PlaySound(string soundName)
        {
            PlaySound(soundName, 1.0f);
        }

        public static void PlaySound(string soundName, float volume)
        {
            var filename = $"Content/Sounds/{ soundName }.mp3";
            var input = new PlayOnce(new AudioFileReader(filename));

            if (_music != null)
            {
                _music.AddDampener();
                input.OnSoundFinished += OnSoundFinished;
            }

            AudioPlaybackEngine.Instance.Play(input);
        }

        private static void OnSoundFinished(object sender, EventArgs e)
        {
            ((PlayOnce)sender).OnSoundFinished -= OnSoundFinished;
            _music.RemoveDampener();
        }

        public static void PlayMusicOnce(string songName)
        {
            PlayMusicOnce(songName, 0.5f);
        }

        public static void PlayMusicOnce(string songName, float volume)
        {
            TransitionToSong(volume, new PlayOnce($"Content/{ songName }.mp3"));
        }

        public static void PlayMusic(string songName)
        {
            PlayMusic(songName, 0.5f);
        }

        public static void PlayMusic(string songName, float volume)
        {
            TransitionToSong(volume, new Looping($"Content/{ songName }.mp3"));
        }

        public static void StopMusic()
        {
            PlayMusic("Music/mute", 0);
        }

        private static void TransitionToSong(float volume, ISampleProvider song)
        {
            if (_music == null)
                _music = new Dampening(song, volume);
            else
            {
                var old = _music;
                _music = new Dampening(song, volume, old.Dampeners);
                old.Volume = 0;
            }
            AudioPlaybackEngine.Instance.Play(_music);
        }
    }
}
