using System;
using AVFoundation;
using Foundation;
using MeditSolution.Service;

namespace MeditSolution.iOS.Services
{
    public class PlayNotificationSound : IPlayNotificationSound
    {
        private AVAudioPlayer _audioPlayer = null;

        public void PlaySound()
        {
            try
            {
                _audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename("notification.caf"));
                _audioPlayer.PrepareToPlay();
                _audioPlayer.Play();
            }
            catch (Exception)
            {

            }
        }
    }
}
