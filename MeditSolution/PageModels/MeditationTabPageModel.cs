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
		public string Grey { get; set; } = "#9b9b9b";

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
            //IsLoading = Seances.Any() ? false : true;

            if (IsLoading)
                return;

            IsLoading = true;

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
					user.MeditationsDone.Add(new MeditationsDone() { id = current_meditation.Id });
				}

				await StoreManager.UserStore.UpdateCurrentUser(user);
				//

				MeditationsDone current_meditation_progress = user.MeditationsDone.Where((arg) => arg.id == current_meditation.Id).First();

				//Setting main meditation details show at bottom of page
				Meditation = Settings.DeviceLanguage == "English" ? current_meditation.Label_EN : current_meditation.Label;
				MeditationDetail = $"{(GetSeanceCount(current_meditation) + (GetSeanceCount(current_meditation) == 3 ? 1:0 ))} {AppResources.seances} - {current_meditation.Length / 60} min";
				MeditationIcon = Constants.FileUrl + "files" + current_program.Icon;

				if (!string.IsNullOrEmpty(current_program.Color) && current_program.Color.Contains("#"))
					Tint = current_program.Color;
				//   

				Seances = new ObservableCollection<object>();

				//LEVEL 1
				if (current_meditation.Level1FrWoman != null)
				{
                    var is_downloaded = await StoreManager.MeditationStore.IsAvailableOffline(current_meditation.Id + "1"+ Settings.Voice + Settings.Language);

                    Seances.Add(new SeancesModel(current_meditation, 1, Tint) { IsDownloaded = is_downloaded.Item1, IsLocked = false, Model = this });
				}

				if (current_meditation.Level2FrWoman != null)
				{
                    var is_downloaded = await StoreManager.MeditationStore.IsAvailableOffline(current_meditation.Id + "2"+ Settings.Voice + Settings.Language);

					Seances.Add(Grey); // connecting line
                    Seances.Add(new SeancesModel(current_meditation, 2, Tint) { IsDownloaded = is_downloaded.Item1, IsLocked = !current_meditation_progress.level1Done, Model = this });
				}

				if (current_meditation.Level3FrWoman != null)
				{                  
                    var is_downloaded = await StoreManager.MeditationStore.IsAvailableOffline(current_meditation.Id + "3"+ Settings.Voice + Settings.Language);

					Seances.Add(Grey); // connecting line
                    Seances.Add(new SeancesModel(current_meditation, 3, Tint) { IsDownloaded = is_downloaded.Item1, IsLocked = !current_meditation_progress.level2Done, Model = this });
				}

				if (Seances.Any() && GetSeanceCount(current_meditation) == 3)
				{
					Seances.Add(Grey); // connecting line
					Seances.Add(new SeancesModel(current_meditation, 4, Tint) { IsDownloaded = false, IsLocked = !current_meditation_progress.level3Done, Model = this });

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

        public int GetSeanceCount(Meditation meditation)
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

        public MeditationFile GetMeditationFileForUser(Meditation meditation, int level)
        {
            if (meditation == null)
                return null;

            if (level == 1)
            {
                if (Settings.Language == "en")
                {
                    if (Settings.Voice == "male")
                    {
                        return meditation.Level1EnMan;
                    }
                    else
                    {
                        return meditation.Level1EnWoman;
                    }
                }
                else
                {
                    if (Settings.Voice == "male")
                    {
                        return meditation.Level1FrMan;
                    }
                    else
                    {
                        return meditation.Level1FrWoman;
                    }
                }
            }
            else if (level == 2)
            {
                if (Settings.Language == "en")
                {
                    if (Settings.Voice == "male")
                    {
                        return meditation.Level2EnMan;
                    }
                    else
                    {
                        return meditation.Level2EnWoman;
                    }
                }
                else
                {
                    if (Settings.Voice == "male")
                    {
                        return meditation.Level2FrMan;
                    }
                    else
                    {
                        return meditation.Level2FrWoman;
                    }
                }
            }
            else if (level == 3)
            {
                if (Settings.Language == "en")
                {
                    if (Settings.Voice == "male")
                    {
                        return meditation.Level3EnMan;
                    }
                    else
                    {
                        return meditation.Level3EnWoman;
                    }
                }
                else
                {
                    if (Settings.Voice == "male")
                    {
                        return meditation.Level3FrMan;
                    }
                    else
                    {
                        return meditation.Level3FrWoman;
                    }
                }
            }
            else
                return null;
        }
  
    }
}
