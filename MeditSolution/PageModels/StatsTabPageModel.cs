﻿using System;
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
        public bool Play { get; set; }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            IsLoading = user == null;

            var _user = await StoreManager.UserStore.GetCurrentUser();

            if (_user == null)
            {
                await ToastService.Show(AppResources.requestfailed);
                IsLoading = false;
                return;
            }

            user = _user;

            if (Settings.TimeSecondsOffline != 0)
            {
                if (user.TotalMinutesMeditated.HasValue)
                {
                    var minutes = user.TotalMinutesMeditated.HasValue ? user.TotalMinutesMeditated.Value : 0;
                    minutes += (Settings.TimeSecondsOffline / 60);
                    user.TotalMinutesMeditated = minutes;
                }
            }

            Name = $"{user.Firstname} {user.Lastname}";

            Icon = "level" + (user.CurrentLevel + 1) + ".json";

            if (string.IsNullOrWhiteSpace(Name))
                Name = user.Email;

            Evolution = AppResources.evolution + " " + (user.CurrentLevel + 1);

            MeditationTime = user.TotalMinutesMeditated.HasValue ? $"{user.TotalMinutesMeditated.Value / 60}h{user.TotalMinutesMeditated.Value % 60}min" : "0 min";

            CurrentMax = user.MaxDaysMeditation.HasValue ? user.MaxDaysMeditation?.ToString() : "0";

            RecordMax = user.RecordMaxDaysMeditation.HasValue ? user.RecordMaxDaysMeditation?.ToString() : "0";

            IsLoading = false;
        }

	}
}
