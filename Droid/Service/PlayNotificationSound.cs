using System;
using Android.App;
using Android.Media;
using MeditSolution.Droid.Service;
using MeditSolution.Service;

[assembly: Xamarin.Forms.Dependency(typeof(PlayNotificationSound))]
namespace MeditSolution.Droid.Service
{
    public class PlayNotificationSound : IPlayNotificationSound
    {
        MediaPlayer _player;

        public void PlaySound()
        {
            _player = MediaPlayer.Create(Application.Context, Resource.Raw.notification);
            _player.Start();
        }

    }
}
