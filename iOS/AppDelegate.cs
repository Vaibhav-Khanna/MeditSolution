using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Facebook.CoreKit;
using FFImageLoading.Forms.Touch;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using Plugin.MediaManager.Forms.iOS;
using ProgressRingControl.Forms.Plugin.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Essentials;
using Plugin.CrossPlatformTintedImage.iOS;
using Plugin.MediaManager;
using Plugin.HtmlLabel.iOS;

namespace MeditSolution.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Settings.AppID = "1840011542984981";
            Settings.DisplayName = "Xama­r­i­n­F­o­r­m­s­N­a­t­i­v­e­L­ogin";

			Rox.VideoIos.Init();

			HtmlLabelRenderer.Initialize();
           
            global::Xamarin.Forms.Forms.Init();

            Rg.Plugins.Popup.IOS.Popup.Init();

			TintedImageRenderer.Init();

            CrossMediaManager.Current = new MediaManagerImplementation();

			ProgressRingRenderer.Init();

            CarouselViewRenderer.Init();
   
			SfPickerRenderer.Init();

            Akavache.BlobCache.ApplicationName = "Medit";

			AnimationViewRenderer.Init();

            CachedImageRenderer.Init();
            
            LoadApplication(new App());

            ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

			UITabBar.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primaryDark"]).ToUIColor();
            UITabBar.Appearance.SelectedImageTintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primaryDark"]).ToUIColor();

			UISwitch.Appearance.OnTintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primary"]).ToUIColor();

			//UINavigationBar.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primaryDark"]).ToUIColor(); //Tint color of button items
			//UIBarButtonItem.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primaryDark"]).ToUIColor(); //Tint color of button items

            var result = base.FinishedLaunching(app, options);

            UIApplication.SharedApplication.StatusBarHidden = false;

            return result;
        }

        public override void OnActivated(UIApplication uiApplication)  
        {  
            base.OnActivated(uiApplication);  
            AppEvents.ActivateApp();  
        }  
   
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)  
        {  
            //return base.OpenUrl(application, url, sourceApplication, annotation);  
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);  
        }
    }


	[Register("MeditSolutionForm")]
    public class MediaFormsApplication : UIApplication
    {
        private MediaManagerImplementation MediaManager => (MediaManagerImplementation)CrossMediaManager.Current;

        public override void RemoteControlReceived(UIEvent theEvent)
        {
            MediaManager.MediaRemoteControl.RemoteControlReceived(theEvent);
        }
    }
}
