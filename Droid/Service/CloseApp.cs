using System;
using Android.OS;
using Android.Views;
using MeditSolution.Droid.Service;
using MeditSolution.Service;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(CloseApp))]
namespace MeditSolution.Droid.Service
{
	public class CloseApp : ICloseApp
    {       
        void ICloseApp.CloseApp()
        {            
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivity(typeof(MainActivity));
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Finish();
        }
    }
}
