using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.DataStore.Implementation.Stores;
using MeditSolution.Helpers;

namespace MeditSolution.PageModels
{
	public class SubscriptionPageModel : BasePageModel
    {
		public string MonthlyFeeText { get; set; } 
		public string AnnualFeeText { get; set; } 
		public string SubscribeButtonText { get; set; } = AppResources.isubscribe;
		public bool? IsMonthlySelected { get; set; } = null;

		public bool IsSubscribed { get; set; }

		public string SubscribedTenure { get; set; } 
		public string SubscribedPrice { get; set; }
		public string SubscribedDate { get; set; }

		private SubscriptionStore SubscriptionStore = new SubscriptionStore();

		public override void Init(object initData)
		{
			base.Init(initData);

			LoadDataFromStore();
		}

		async void LoadDataFromStore()
		{
			IsLoading = true;

			var products = await SubscriptionStore.GetSubscriptionInfo();
            
			if(products!=null)
			foreach (var item in products)
			{
				if(item.ProductId == SubscriptionStore.MonthlySubscriptionID)
				{  // monthly data
					MonthlyFeeText = item.LocalizedPrice + "/" + AppResources.month;
				}

				if(item.ProductId == SubscriptionStore.AnnuallySubscriptionID)
				{
					AnnualFeeText = item.LocalizedPrice + "/" + AppResources.year;
				}
			}          

			var purchasedGoods = await SubscriptionStore.CheckAndUpdateSubscriptionStatus();
            
			if (purchasedGoods != null)
			{
				IsSubscribed = true;

				if (purchasedGoods.Item2.ProductId == SubscriptionStore.MonthlySubscriptionID)
				{
					SubscribedTenure = AppResources.monthly;
					SubscribedDate = AppResources.subscriptionstarted + " "+  purchasedGoods?.Item2.TransactionDateUtc.Date.ToString("D");
					SubscribedPrice = MonthlyFeeText;
				}
				else if (purchasedGoods.Item2.ProductId == SubscriptionStore.AnnuallySubscriptionID)
				{
					SubscribedTenure = AppResources.annualy;
					SubscribedDate = AppResources.subscriptionstarted + " " + purchasedGoods?.Item2.TransactionDateUtc.Date.ToString("D");
					SubscribedPrice = AnnualFeeText;
				}
			}

			IsLoading = false;
		}
		public Command OptionSelectedCommand => new Command(() =>
		{
			SubscribeButtonText = AppResources.isubscribe + (IsMonthlySelected.HasValue ? IsMonthlySelected.Value ? (" - " + MonthlyFeeText) : (" - " + AnnualFeeText) : "");
		});
        
		public Command SubscribeCommand => new Command(async() =>
		{
			if (IsMonthlySelected.HasValue)
			{
				//API NATIVE APP STORE
				string membership = IsMonthlySelected.Value ? SubscriptionStore.MonthlySubscriptionID : SubscriptionStore.AnnuallySubscriptionID;

				Dialog.ShowLoading();

				var Subs_response = await SubscriptionStore.PurchaseSubscription(membership);

				Dialog.HideLoading();

				if (Subs_response)
				{
					await CoreMethods.DisplayAlert(
						AppResources.SubscriptionThanks, null,
						AppResources.ok);

					LoadDataFromStore();
				}
				else
				{
					await CoreMethods.DisplayAlert(null, AppResources.ErrorMessageSubscription, AppResources.ok);
				}            
			}
		});

		public Command UnSubscribeCommand => new Command(async() =>
		{
			bool answer = await CoreMethods.DisplayAlert(Device.RuntimePlatform == Device.iOS ?
			                                             AppResources.UnsubscribeWarning : null,
			                                             Device.RuntimePlatform == Device.Android ? AppResources.UnsubscribeWarning : null,
			                                             AppResources.yes,
			                                             AppResources.NotNow);

			if (answer)
			{
				if (Device.RuntimePlatform == Device.iOS)
					Plugin.Settings.CrossSettings.Current.OpenAppSettings();
				else
				{
					Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.meditsolutions.mobile"));
				}
			}
		});
        
		public Command RestorePurchaseCommand => new Command(() =>
		{
			LoadDataFromStore();
		});
       
	}
}
