using Plugin.SimpleAudioPlayer;

namespace SeniorCare.Resources
{
    public static class AudioPlayer
    {
        private static readonly ISimpleAudioPlayer _player;

        static AudioPlayer()
        {
            _player = CrossSimpleAudioPlayer.Current;
        }

        public static void PlayNotification()
        {
            _player.Load("notificationSound.mp3");
            _player.Play();
        }
        
    }
}
