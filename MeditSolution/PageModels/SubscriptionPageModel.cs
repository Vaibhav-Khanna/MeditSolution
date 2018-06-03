using System;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
	public class SubscriptionPageModel : BasePageModel
    {
		public string MonthlyFeeText { get; set; } = "6,99 €/"+AppResources.month;
		public string AnnualFeeText { get; set; } = "59,99€/"+ AppResources.year;
		public string SubscribeButtonText { get; set; } = AppResources.isubscribe;
		public bool? IsMonthlySelected { get; set; } = null;

		public bool IsSubscribed { get; set; }

		public string SubscribedTenure { get; set; } = AppResources.monthly;
		public string SubscribedPrice { get; set; } = "6,99€ / "+AppResources.month;
		public string SubscribedDate { get; set; } = AppResources.subscriptionstarted + " 09/03/2018";

		public override void Init(object initData)
		{
			base.Init(initData);
		}

		public Command OptionSelectedCommand => new Command(() =>
		{
			SubscribeButtonText = AppResources.isubscribe + (IsMonthlySelected.HasValue ? IsMonthlySelected.Value ? (" - " + MonthlyFeeText) : (" - " + AnnualFeeText) : "");
		});

		public Command SubscribeCommand => new Command(() =>
		{
			if(IsMonthlySelected.HasValue)
			IsSubscribed = true;
		});

		public Command UnSubscribeCommand => new Command(() =>
		{
			IsSubscribed = false;
		});
	}
}
