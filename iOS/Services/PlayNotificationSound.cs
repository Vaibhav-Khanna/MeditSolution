using System;
using AVFoundation;
using Foundation;
using MeditSolution.iOS.Services;
using MeditSolution.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlayNotificationSound))]
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
