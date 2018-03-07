using NAudio.Wave;
using System;

namespace MonoDragons.Core.Audio
{
    public static class Audio
    {
        private static Dampening _backgroundMusic;

        public static void PlaySound(string name)
        {
            PlaySound(name, 1.0f);
        }

        public static void PlaySound(string name, float volume)
        {
            AudioPlayer.Instance.Play(new PlayOnce($"Content/Sounds/{ name }.mp3"));
        }

        public static void PlayMusicEffect(string name)
        {
            PlaySound(name, 1.0f);
        }

        public static void PlayMusicEffect(string name, float volume)
        {
            var input = new PlayOnce($"Content/Sounds/{ name }.mp3");

            if (_backgroundMusic != null)
            {
                _backgroundMusic.AddDampener();
                input.OnSoundFinished += OnMusicEffectFinished;
            }

            AudioPlayer.Instance.Play(input);
        }

        private static void OnMusicEffectFinished(object sender, EventArgs e)
        {
            ((PlayOnce)sender).OnSoundFinished -= OnMusicEffectFinished;
            _backgroundMusic.RemoveDampener();
        }

        public static void PlayMusicOnce(string name)
        {
            PlayMusicOnce(name, 0.5f);
        }

        public static void PlayMusicOnce(string name, float volume)
        {
            TransitionToSong(volume, new PlayOnce($"Content/{ name }.mp3"));
        }

        public static void PlayMusic(string name)
        {
            PlayMusic(name, 0.5f);
        }

        public static void PlayMusic(string name, float volume)
        {
            TransitionToSong(volume, new Looping($"Content/{ name }.mp3"));
        }

        public static void StopMusic()
        {
            PlayMusic("Music/mute", 0);
        }

        private static void TransitionToSong(float volume, ISampleProvider song)
        {
            if (_backgroundMusic == null)
                _backgroundMusic = new Dampening(song, volume);
            else
            {
                var old = _backgroundMusic;
                _backgroundMusic = new Dampening(song, volume, old.Dampeners);
                old.Volume = 0;
            }
            AudioPlayer.Instance.Play(_backgroundMusic);
        }
    }
}
