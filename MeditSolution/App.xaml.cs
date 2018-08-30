using Xamarin.Forms;
using FreshMvvm;
using MeditSolution.PageModels;
using System;
using MeditSolution.Controls;
using MeditSolution.Helpers;
using DLToolkit.Forms.Controls;
using MeditSolution.DataStore.Implementation;
using System.Collections.Generic;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using MeditSolution.DataStore.Implementation.Stores;
using Plugin.Notifications;
using Newtonsoft.Json;
using MeditSolution.Models;
using MeditSolution.Resources;
using System.Linq;

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
            
            OneSignal.Current.StartInit(Constants.OneSignalID).Settings(new Dictionary<string, bool>() {
               { IOSSettings.kOSSettingsKeyAutoPrompt, false }}).InFocusDisplaying(OSInFocusDisplayOption.Notification).EndInit();

            Init();
        }

        async void Init()
        {
            new LanguageService().SetLanguage();

            if (Settings.IsLoggedIn)
            {
                var storeManager = new BasePageModel().StoreManager;

                if (StoreManager.NeedsTokenRefresh())
                {
                    var isRefreshed = await storeManager.RegenerateToken();
                    
                    if (isRefreshed)
                    {
						if (Settings.HasToCompleteChat)
						{
							// open chat
							var page = FreshPageModelResolver.ResolvePageModel<ChatPageModel>();
                            MainPage = new FreshNavigationContainer(page);
						}
						else
						{
							await storeManager.UserStore.UpdateCurrentUser(null);
							MainPage = TabNavigator.GenerateTabPage();
						}
                    }
                    else
                    {
                        var page = FreshPageModelResolver.ResolvePageModel<TutorialPageModel>();
                        MainPage = new FreshNavigationContainer(page);
                    }
                }
                else
                {
					if (Settings.HasToCompleteChat)
					{
                        // open chat
						var page = FreshPageModelResolver.ResolvePageModel<ChatPageModel>();
                        MainPage = new FreshNavigationContainer(page);
					}
					else
					{
						await storeManager.UserStore.UpdateCurrentUser(null);
						MainPage = TabNavigator.GenerateTabPage();
					}
                }

				UpdateSubscription();
            }
            else
            {
                var page = FreshPageModelResolver.ResolvePageModel<TutorialPageModel>();
                MainPage = new FreshNavigationContainer(page);
            }
        }

        protected override void OnStart()
        {
            new LanguageService().SetLanguage();
        }

        protected override void OnSleep()
        {
            ApplicationIsPaused?.Invoke(true);
        }

        protected override void OnResume()
        {
            new LanguageService().SetLanguage();

            ApplicationIsPaused?.Invoke(false);
        }

        public static Action<string> PostSuccessFacebookAction { get; set; }

        public static Action<bool> ApplicationIsPaused { get; set; }
		
        public static bool OpenReminders = false;

		async void UpdateSubscription()
		{
			var subscriptionStore = new SubscriptionStore();
			await subscriptionStore.CheckAndUpdateSubscriptionStatus();
			subscriptionStore.Dispose();
		}

       
    }
}
