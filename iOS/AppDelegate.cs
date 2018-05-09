using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Facebook.CoreKit;
using FFImageLoading.Forms.Touch;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using Plugin.MediaManager.Forms.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;

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
            	
            global::Xamarin.Forms.Forms.Init();

            CarouselViewRenderer.Init();

			AnimationViewRenderer.Init();

            CachedImageRenderer.Init();
            
            LoadApplication(new App());

            ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

			UITabBar.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primary"]).ToUIColor();
            UITabBar.Appearance.SelectedImageTintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primary"]).ToUIColor();

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
}
