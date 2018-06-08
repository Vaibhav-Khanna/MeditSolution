using System;
using MeditSolution.iOS.Services;
using MeditSolution.Service;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(AndroidStatusBarColor))]
namespace MeditSolution.iOS.Services
{
	public class AndroidStatusBarColor : IAndroidStatusBarColor
	{

		public void ChangeColor(Color color, bool selfDark = false)
		{
			//if (selfDark)
			//{
			//	// change button tint to white
			//	Device.BeginInvokeOnMainThread(() =>
			//	{
			//		UINavigationBar.Appearance.TintColor = ((Xamarin.Forms.Color.White)).ToUIColor(); //Tint color of button items
			//		UIBarButtonItem.Appearance.TintColor = ((Xamarin.Forms.Color.White)).ToUIColor(); //Tint color of button items
			//	});
			//}
			//else
			//{
			//	// change to default
			//	Device.BeginInvokeOnMainThread(() =>
			//	{
			//		UINavigationBar.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primaryDark"]).ToUIColor(); //Tint color of button items
   //                 UIBarButtonItem.Appearance.TintColor = ((Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["primaryDark"]).ToUIColor(); //Tint color of button items
			//	});
			//}
		}
	}
}