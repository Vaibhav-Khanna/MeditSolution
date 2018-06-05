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

namespace MeditSolution.Droid
{
    [Activity(Label = "Medit'Solution", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
			global::Xamarin.Forms.Forms.Init(this, bundle);

			TintedImageRenderer.Init();

			CrossCurrentActivity.Current.Init(this, bundle);

		    var s = new SfPickerRenderer();

            CarouselViewRenderer.Init();
          
            AnimationViewRenderer.Init();

            CachedImageRenderer.Init(false);

            LoadApplication(new App());
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }
    }

}
