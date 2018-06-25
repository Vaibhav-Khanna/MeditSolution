using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Xamarin.Facebook;
using Xamarin.Forms;
using Lottie.Forms.Droid;
using Plugin.MediaManager.Forms.Android;
using Plugin.CurrentActivity;
using Syncfusion.SfPicker.XForms.Droid;
using Plugin.InAppBilling;
using Plugin.CrossPlatformTintedImage.Android;
using Plugin.HtmlLabel.Android;
using Plugin.MediaManager;
using Akavache;
using Plugin.Notifications;
using Plugin.MediaManager.MediaSession;
using Plugin.MediaManager.ExoPlayer;

namespace MeditSolution.Droid
{
    [Activity(Label = "Medit'Solutions", Icon = "@drawable/icon", Theme = "@style/MyTheme", LaunchMode = LaunchMode.SingleTask, MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IBlobCache cache => Akavache.BlobCache.UserAccount;
        Type s = typeof(Xamarin.Essentials.Browser);

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;

            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
           
			HtmlLabelRenderer.Initialize();

			global::Xamarin.Forms.Forms.Init(this, bundle);

            CrossCurrentActivity.Current.Init(this, bundle);

            if (CrossMediaManager.Current == null)
            CrossMediaManager.Current = new MediaManagerImplementation();

            // use custom Android notifications
            CrossMediaManager.Current.MediaNotificationManager = new PVLMediaNotificationManager(Android.App.Application.Context, typeof(ExoPlayerAudioService));
            //CrossMediaManager.Current.MediaNotificationManager = new PVLMediaNotificationManager(Android.App.Application.Context, typeof(MediaPlayerService));

            // use exoPlayer
            MediaManagerImplementation current = CrossMediaManager.Current as MediaManagerImplementation;
            var exoPlayer = new ExoPlayerAudioImplementation(current.MediaSessionManager);
            CrossMediaManager.Current.AudioPlayer = exoPlayer;

            CrossNotifications.Current.GetType();

			TintedImageRenderer.Init();

		    var s = new SfPickerRenderer();

            VideoViewRenderer.Init();

            CarouselViewRenderer.Init();
          
            AnimationViewRenderer.Init();

            CachedImageRenderer.Init(false);

            BlobCache.ApplicationName = "Medit";

            // prevent screen from sleeping 
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            LoadApplication(new App());
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }
    }

}
