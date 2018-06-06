using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;
using MeditSolution.Resources;
using Xamarin.Essentials;
using FreshMvvm;

namespace MeditSolution.PageModels
{
    public class PlusTabPageModel : BasePageModel
    {
        public ObservableCollection<object> Menu { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Menu = new ObservableCollection<object>();

            Menu.Add(new MenuModel() { Text = AppResources.myaccount, Image = "account.png" });
            Menu.Add(new MenuModel() { Text = AppResources.subscription, Image = "plans.png" });
            Menu.Add(AppResources.mediter);
            Menu.Add(new MenuModel() { Text = AppResources.silentmeditation, Image = "meditationsilenceCopy.png" });
            Menu.Add(new MenuModel() { Text = AppResources.coherentbreathing, Image = "coherencecardiaque.png" });
            Menu.Add(new MenuModel() { Text = AppResources.menureminder, Image = "rappels.png" });
            Menu.Add(new MenuModel() { Text = AppResources.videos, Image = "play.png" });
            Menu.Add(new MenuModel() { Text = AppResources.settings, Image = "settings.png" });
            Menu.Add("");
            Menu.Add(new MenuModel() { Text = AppResources.disconnect, Image = "logout.png" });

            Menu.Add("");
            Menu.Add(new MenuModel() { Text = AppResources.cgu, Image = "" });
        }

        public Command MenuCommand => new Command(async (str) =>
        {
			if (str != null && str is string)
			{
				if (str.ToString() == AppResources.myaccount)
					await CoreMethods.PushPageModel<MyAccountPageModel>();

				if (str.ToString() == AppResources.subscription)
					await CoreMethods.PushPageModel<SubscriptionPageModel>();

				if (str.ToString() == AppResources.videos)
					await CoreMethods.PushPageModel<VideosPageModel>();

				if (str.ToString() == AppResources.silentmeditation)
					await CoreMethods.PushPageModel<MeditationSilentPageModel>();

				if (str.ToString() == AppResources.coherentbreathing)
					await CoreMethods.PushPageModel<MeditationBreathePageModel>();

				if (str.ToString() == AppResources.settings)
					await CoreMethods.PushPageModel<SettingsPageModel>(data: false);

           
            if (str.ToString() == AppResources.disconnect)
            {
                var response = await CoreMethods.DisplayAlert("Déconnexion", "Voulez-vous continuer ?", "Oui", "Annuler");

                if (response)
                {
			        StoreManager.LogoutAsync();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        var page = FreshPageModelResolver.ResolvePageModel<TutorialPageModel>();
                        Application.Current.MainPage = new FreshNavigationContainer(page);
                    });
                }                  
            }



				if (str.ToString() == AppResources.menureminder)
					await CoreMethods.PushPageModel<RemindersPageModel>();

				if (str.ToString() == AppResources.cgu)
				{
					await Browser.OpenAsync(Constants.CGURedirectUrl, BrowserLaunchType.SystemPreferred);
				}
			}
        });

    }
}
