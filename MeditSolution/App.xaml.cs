using Xamarin.Forms;
using FreshMvvm;
using MeditSolution.PageModels;
using System;
using MeditSolution.Controls;
using MeditSolution.Helpers;
using DLToolkit.Forms.Controls;
using MeditSolution.DataStore.Implementation;
using MeditSolution.DataStore.Abstraction;

namespace MeditSolution
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

			FlowListView.Init();

			BasePageModel.Initialize();
                     
			BasePageModel.DefaultNavigationBackgroundColor();
            
			MainPage = new ContentPage();

			Init();
        }

		async void Init()
		{
			new LanguageService().SetLanguage();

			if(Settings.IsLoggedIn)
			{
				var storeManager = new BasePageModel().StoreManager;

				if (StoreManager.NeedsTokenRefresh())
				{
					var isRefreshed = await storeManager.RegenerateToken();

					if (isRefreshed)
					{
						await storeManager.UserStore.UpdateCurrentUser();
						MainPage = TabNavigator.GenerateTabPage();
					}
					else
					{
						var page = FreshPageModelResolver.ResolvePageModel<TutorialPageModel>();
						MainPage = new FreshNavigationContainer(page);
					}
				}
				else
				{
					await storeManager.UserStore.UpdateCurrentUser();
					MainPage = TabNavigator.GenerateTabPage();
				}
			}
			else
			{
				var page = FreshPageModelResolver.ResolvePageModel<TutorialPageModel>();
                MainPage = new FreshNavigationContainer(page);
			}
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Action<string> PostSuccessFacebookAction { get; set; } 
  
    }
}
