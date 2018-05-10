using System;
using FreshMvvm;
using Xamarin.Forms;
using MeditSolution.Service;

namespace MeditSolution.PageModels
{
    public class BasePageModel : FreshBasePageModel
    {

		public static void ChangeNavigationBackgroundColor(Color backColor)
		{
			Application.Current.Resources["backgroundColor"] = backColor;
			Application.Current.Resources["textColor"] = Color.White;
			if (Device.RuntimePlatform == Device.Android)
			{
				IAndroidStatusBarColor AndroidStatusBarColor = DependencyService.Get<IAndroidStatusBarColor>();
				AndroidStatusBarColor.ChangeColor(backColor, true);
			}
		}

		public static void DefaultNavigationBackgroundColor()
		{
			Application.Current.Resources["backgroundColor"] = Device.RuntimePlatform == Device.iOS ? Color.White : Application.Current.Resources["primary"];
			Application.Current.Resources["textColor"] = Device.RuntimePlatform == Device.iOS ? Color.Black : Color.White;

			if (Device.RuntimePlatform == Device.Android)
			{
				IAndroidStatusBarColor AndroidStatusBarColor = DependencyService.Get<IAndroidStatusBarColor>();
				AndroidStatusBarColor.ChangeColor((Color)Application.Current.Resources["primaryDark"]);
			}
		}
    }
}
