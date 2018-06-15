using System;
using MeditSolution.Models;
using MeditSolution.Models.DataObjects;
using Xamarin.Forms;
using MeditSolution.Helpers;
using Plugin.MediaManager;
using System.Linq;
using MeditSolution.Resources;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Implementations;

namespace MeditSolution.PageModels
{
	public class MeditationPlayPageModel : BasePageModel
    {
		public double Progress { get; set; }
		public string TimerText { get; set; }
		SeancesModel SeanceModel;
        IAudioPlayer AudioPlayer => CrossMediaManager.Current.AudioPlayer;
		public Color Tint { get; set; }
		public Color TintDark { get; set; }

		TimeSpan position;
		MediaFile file;

		public Command PlayPauseCommand => new Command(async() =>
		{
            if (AudioPlayer.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Playing)
            {
                position = AudioPlayer.Position;
                await AudioPlayer.Pause();
            }
            else if (AudioPlayer.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Paused)
            {
                await AudioPlayer.Play();
            }
		}); 
        

        public async override void Init(object initData)
		{
			base.Init(initData);

            if (initData is SeancesModel)
            {
                IsLoading = true;

                SeanceModel = ((SeancesModel)initData);

                Tint = Color.FromHex(SeanceModel.Tint.Substring(1));
                TintDark = ((Tint).AddLuminosity(-0.2));

                MeditationFile meditationFile = GetMeditationFileForUser(SeanceModel.Meditation, SeanceModel.Level);

                if (meditationFile == null)
                {
                    IsLoading = false;
                    return;
                }

                AudioPlayer.PlayingChanged += AudioPlayer_PlayingChanged;
                AudioPlayer.MediaFailed += AudioPlayer_MediaFailed;
                AudioPlayer.MediaFinished += AudioPlayer_MediaFinished;
                AudioPlayer.StatusChanged += AudioPlayer_StatusChanged;

                var url = Constants.RestUrl + "file/" + meditationFile.Path;

                if (SeanceModel.IsDownloaded)
                {
                    var data = await StoreManager.MeditationStore.IsAvailableOffline(SeanceModel.Meditation.Id+SeanceModel.Level + Settings.Voice + Settings.Language);

                    if(data!=null && data.Item1 && data.Item2!=null)
                    {
                        var pathToFileURL = new System.Uri(data.Item2).AbsolutePath;

                        await AudioPlayer.Play(file = new MediaFile("file://" + pathToFileURL));
                    }
                    else
                    {
                        await AudioPlayer.Play(file = new MediaFile(url, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, Plugin.MediaManager.Abstractions.Enums.ResourceAvailability.Remote));
                    }  
                }
                else
                {
                    await AudioPlayer.Play(file = new MediaFile(url, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, Plugin.MediaManager.Abstractions.Enums.ResourceAvailability.Remote));
                }
            }
   		}

		void AudioPlayer_StatusChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.StatusChangedEventArgs e)
		{
			if(e.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Paused)
			{
				IsPlaying = false;
			}
			else if(e.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Playing)
			{
				IsLoading = false;
				IsPlaying = true;
			}
		}

		void AudioPlayer_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
		{
			Progress = Device.RuntimePlatform == Device.iOS ? e.Progress : e.Progress/100;
			TimerText = e.Duration.Subtract(e.Position).ToString("hh':'mm':'ss");
		}

		async void AudioPlayer_MediaFailed(object sender, Plugin.MediaManager.Abstractions.EventArguments.MediaFailedEventArgs e)
		{
			await ToastService.Show("Cannot play this file");
		}

		async void AudioPlayer_MediaFinished(object sender, Plugin.MediaManager.Abstractions.EventArguments.MediaFinishedEventArgs e)
		{
			Dialog.ShowLoading();

			var user = await StoreManager.UserStore.UpdateCurrentUser(StoreManager.UserStore.User);

			var isAdded = await StoreManager.MeditationStore.AddMeditationTimeAsync((int)SeanceModel.Meditation.Length);

			var meditiondone = user?.MeditationsDone?.Where((arg) => arg.id == SeanceModel?.Meditation.Id).First();

			if (meditiondone != null)
			{
				if (SeanceModel.Level == 1)
				{
					meditiondone.level1Done = true;
				}
				else if (SeanceModel.Level == 2)
				{
					meditiondone.level2Done = true;
				}
				else if (SeanceModel.Level == 3)
				{
					meditiondone.level3Done = true;
				}

				user = await StoreManager.UserStore.UpdateCurrentUser(user);
			}

			Dialog.HideLoading();

			if (GetSeanceCount(SeanceModel.Meditation) == SeanceModel.Level)
			{
				await CoreMethods.PushPageModel<MeditationEndPageModel>(true, modal: true);
				CoreMethods.RemoveFromNavigation<MeditationPlayPageModel>(true);
			}
			else
				await CoreMethods.PopPageModel(animate: false);
		}

		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
			base.ViewIsDisappearing(sender, e);

			AudioPlayer?.Stop();

			if (AudioPlayer != null)
			{
				AudioPlayer.PlayingChanged -= AudioPlayer_PlayingChanged;
				AudioPlayer.MediaFailed -= AudioPlayer_MediaFailed;
				AudioPlayer.MediaFinished -= AudioPlayer_MediaFinished;
				AudioPlayer.StatusChanged -= AudioPlayer_StatusChanged;
			}
		}

		MeditationFile GetMeditationFileForUser(Meditation meditation, int level)
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

		int GetSeanceCount(Meditation meditation)
        {
            int seanceCount = 0;

            if (meditation.Level1FrWoman != null)
                seanceCount += 1;
            if (meditation.Level2FrWoman != null)
                seanceCount += 1;
            if (meditation.Level3FrWoman != null)
                seanceCount += 1;

			if (seanceCount == 3)
				seanceCount += 1;

            return seanceCount;
        }

		public Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;
            
            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

			return Color.FromRgb((int)red, (int)green, (int)blue );
        }
	}
}
