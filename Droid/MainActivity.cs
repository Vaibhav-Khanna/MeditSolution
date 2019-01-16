using System;
using Akavache;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Lottie.Forms.Droid;
using Plugin.CrossPlatformTintedImage.Android;
using Plugin.CurrentActivity;
using Plugin.HtmlLabel.Android;
using Plugin.InAppBilling;
using Plugin.MediaManager;
using Plugin.MediaManager.ExoPlayer;
using Plugin.MediaManager.Forms.Android;
using Plugin.Notifications;
using Syncfusion.SfPicker.XForms.Droid;

namespace MeditSolution.Droid
{
    [Activity(Label = "Medit'Solutions", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait )]
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

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            Xamarin.Essentials.Platform.Init(this, bundle);

            Rg.Plugins.Popup.Popup.Init(this,bundle);

            //AnimationViewRenderer.Init();

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

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);

            BlobCache.ApplicationName = "Medit";
             
            LoadApplication(new App());
        }


		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }

}
