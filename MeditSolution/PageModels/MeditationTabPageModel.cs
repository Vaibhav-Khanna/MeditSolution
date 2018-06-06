using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using MeditSolution.Models.DataObjects;
using Xamarin.Forms;
using MeditSolution.Helpers;
using MeditSolution.Resources;
using System.Linq;
using Com.OneSignal;

namespace MeditSolution.PageModels
{
    public class MeditationTabPageModel : BasePageModel
    {

        User user;
        public bool IsEmpty { get; set; } = true;
        public ObservableCollection<object> Seances { get; set; } = new ObservableCollection<object>();
        public string Meditation { get; set; } = "";
        public string MeditationDetail { get; set; } = "";
        public string MeditationIcon { get; set; } = "";
        public string Tint { get; set; } = "#50e3c2";       

        public MeditationTabPageModel()
        {
            Com.OneSignal.Abstractions.IdsAvailableCallback callback = new Com.OneSignal.Abstractions.IdsAvailableCallback(HandleIdsAvailableCallback);

            OneSignal.Current.IdsAvailable(callback);   
        }

		async void OpenReminders()
		{
			await CoreMethods.PushPageModel<RemindersPageModel>(animate:false);
		}

        async void HandleIdsAvailableCallback(string playerID, string pushToken)
        {
            if (!string.IsNullOrWhiteSpace(playerID))
            {
                var CurrentUser = await StoreManager.UserStore.GetCurrentUser();

                if (CurrentUser != null)
                {
                    CurrentUser.OneSignalPushId = playerID;
                    
                    var userUpdated = await StoreManager.UserStore.UpdateAsync(CurrentUser);
                }
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
                      
            if (string.IsNullOrEmpty((user = StoreManager.UserStore.User)?.CurrentMeditationId))
            {
                IsEmpty = true;
            }
            else
				IsEmpty = false;

            OneSignal.Current.RegisterForPushNotifications();

            GetMeditation();

			if (App.OpenReminders)
            {
                App.OpenReminders = false;
                OpenReminders();
            }
        }


        async void GetMeditation()
        {

            IsLoading = Seances.Any() ? false : true;

            if (user == null)
            {
                IsLoading = false;
                return;
            }

            var current_meditation = await StoreManager.MeditationStore.GetItemAsync(user.CurrentMeditationId);
            var current_program = await StoreManager.ProgramStore.GetItemAsync(user.CurrentProgramId);


            if (current_meditation != null && current_program != null)
            {
				// add it to user done meditations if not present
				if (user.MeditationsDone == null)
					user.MeditationsDone = new System.Collections.Generic.List<MeditationsDone>();

				if (!user.MeditationsDone.Where((arg) => arg.id == current_meditation.Id).Any())
				{
					user.MeditationsDone.Add(new MeditationsDone(){ id = current_meditation.Id });
				}

				await StoreManager.UserStore.UpdateCurrentUser(user);
				//

				MeditationsDone current_meditation_progress = user.MeditationsDone.Where((arg) => arg.id == current_meditation.Id).First();

                //Setting main meditation details show at bottom of page
                Meditation = Settings.DeviceLanguage == "English" ? current_meditation.Label_EN : current_meditation.Label;
                MeditationDetail = $"{(GetSeanceCount(current_meditation) + 1)} {AppResources.seances} - {current_meditation.Length / 60} min";
				MeditationIcon = Constants.FileUrl + "files" + current_program.Icon;

                if (!string.IsNullOrEmpty(current_program.Color) && current_program.Color.Contains("#"))
                    Tint = current_program.Color;
				//

                Seances = new ObservableCollection<object>();

                //LEVEL 1
                if (current_meditation.Level1FrWoman != null)
                {
					Seances.Add(new SeancesModel(current_meditation, 1) { Tint = Tint, IsDownloaded = false, IsLocked = false, Model = this });
                    Seances.Add(Tint); // connecting line
                }

                if (current_meditation.Level2FrWoman != null)
                {
					Seances.Add(new SeancesModel(current_meditation, 2) { Tint = Tint, IsDownloaded = false, IsLocked = !current_meditation_progress.level1Done , Model = this });
                    Seances.Add(Tint); // connecting line
                }

                if (current_meditation.Level3FrWoman != null)
                {
					Seances.Add(new SeancesModel(current_meditation, 3) { Tint = Tint, IsDownloaded = false, IsLocked = !current_meditation_progress.level2Done, Model = this });
                    Seances.Add(Tint); // connecting line
                }

                if (Seances.Any())
                {
					bool islocked = true;

					var count = GetSeanceCount(current_meditation);

					if (count == 1)
						islocked = !current_meditation_progress.level1Done;
					if (count == 2)
                        islocked = !current_meditation_progress.level2Done;
					if (count == 3)
                        islocked = !current_meditation_progress.level3Done;

					Seances.Add( new SeancesModel(current_meditation, 4) { Tint = Tint, IsDownloaded = false, IsLocked = islocked, Model = this });
     
					Seances.Add("#ffffff"); // connecting line
                    Seances.Add("#ffffff"); // connecting line
                    Seances.Add("#ffffff"); // connecting line
                }              
            }

            IsLoading = false;
        }


		public Command StartCommand => new Command(async() =>
		{
			await CoreMethods.SwitchSelectedTab<CatalogueTabPageModel>();
        });

        int GetSeanceCount(Meditation meditation)
        {
            int seanceCount = 0;

            if (meditation.Level1FrWoman != null)
                seanceCount += 1;
            if (meditation.Level2FrWoman != null)
                seanceCount += 1;
            if (meditation.Level3FrWoman != null)
                seanceCount += 1;

            return seanceCount;
        }
  
    }
}
