using System;
using Xamarin.Forms;


namespace MeditSolution.PageModels
{
	public class SubscriptionPageModel : BasePageModel
    {
		public string MonthlyFeeText { get; set; } = "6,99 €/mois";
		public string AnnualFeeText { get; set; } = "59,99€/an";
		public string SubscribeButtonText { get; set; } = "Je m’abonne";
		public bool? IsMonthlySelected { get; set; } = null;

		public bool IsSubscribed { get; set; }

		public string SubscribedTenure { get; set; } = "Mensuel";
		public string SubscribedPrice { get; set; } = "6,99€ / Mois";
		public string SubscribedDate { get; set; } = "Vous êtes abonné depuis le 09/03/2018";

		public override void Init(object initData)
		{
			base.Init(initData);
		}

		public Command OptionSelectedCommand => new Command(() =>
		{
			SubscribeButtonText = "Je m’abonne" + (IsMonthlySelected.HasValue ? IsMonthlySelected.Value ? (" - " + MonthlyFeeText) : (" - " + AnnualFeeText) : "");
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
