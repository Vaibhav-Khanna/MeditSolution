using System;
using FreshMvvm;
using Xamarin.Forms;
using MeditSolution.Service;
using MeditSolution.DataStore.Abstraction;
using MeditSolution.DataStore.Implementation;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.DataStore.Implementation.Stores;
using MeditSolution.Helpers;

namespace MeditSolution.PageModels
{
    public class BasePageModel : FreshBasePageModel
    {

		bool _playing = true;
        [PropertyChanged.DoNotNotify]
        public bool IsPlaying { get { return _playing; } set { _playing = value; Image = (value) ? "pause.png" : "playbig.png"; RaisePropertyChanged(); } }
        public string Image { get; set; } = "pause.png";

		public IStoreManager StoreManager = FreshIOC.Container.Resolve<IStoreManager>();

		public bool IsLoading { get; set; }

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


		public static void Initialize()
		{
			FreshIOC.Container.Register<IStoreManager, StoreManager>();

			FreshIOC.Container.Register<IMeditationStore, MeditationStore>();
			FreshIOC.Container.Register<IProgramStore, ProgramStore>();
			FreshIOC.Container.Register<IVideoStore, VideoStore>();
			FreshIOC.Container.Register<IUserStore, UserStore>();     
		}


	    
    }
}
