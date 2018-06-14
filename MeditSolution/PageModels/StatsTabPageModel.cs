using System;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;
using MeditSolution.Resources;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class StatsTabPageModel : BasePageModel
    {
		public string Name { get; set; } = " ";
		public string Evolution { get; set; } = " ";
		public string MeditationTime { get; set; } = "...";
		public string CurrentMax { get; set; } = " ";
		public string RecordMax { get; set; } = " ";
		public string Icon { get; set; }
		User user;
              

		protected async override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			IsLoading = user == null;

			var _user = await StoreManager.UserStore.GetCurrentUser();

			if(_user==null)
			{
				await ToastService.Show(AppResources.requestfailed);
				IsLoading = false;
				return;
			}

            if(Settings.TimeSecondsOffline != 0)
            {
                user.TotalMinutesMeditated += (Settings.TimeSecondsOffline/60);
            }

			user = _user;

			Name = $"{user.Firstname} {user.Lastname}";

			if (Device.RuntimePlatform == Device.iOS)
				Icon = "level" + (user.CurrentLevel+1) + ".png";
            else
				Icon = "level_" + (user.CurrentLevel+1) + ".png";

			if (string.IsNullOrWhiteSpace(Name))
				Name = user.Email;

			Evolution = AppResources.evolution + " " + (user.CurrentLevel + 1);

			MeditationTime = user.TotalMinutesMeditated.HasValue ? $"{user.TotalMinutesMeditated.Value/60}h{user.TotalMinutesMeditated.Value%60}min" : "0 min";

			CurrentMax = user.MaxDaysMeditation.HasValue ? user.MaxDaysMeditation?.ToString() : "0";

			RecordMax = user.RecordMaxDaysMeditation.HasValue ? user.RecordMaxDaysMeditation?.ToString() : "0";

			IsLoading = false;
		}

	}
}
