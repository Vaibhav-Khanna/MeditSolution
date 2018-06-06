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


		public Command PlayPauseCommand => new Command(async()=>
		{		 
			if(AudioPlayer.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Playing)
			{
				await AudioPlayer.Pause();
			}
			else if(AudioPlayer.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Paused)
			{
				await AudioPlayer.Play();
			}
		});
        
		public override void Init(object initData)
		{
			base.Init(initData);

			if(initData is SeancesModel)
			{
				IsLoading = true;

				SeanceModel = ((SeancesModel)initData);

				MeditationFile meditationFile = GetMeditationFileForUser(SeanceModel.Meditation,SeanceModel.Level);
                
				if(meditationFile==null)
				{
					IsLoading = false;
					return;
				}
                
				AudioPlayer.PlayingChanged += AudioPlayer_PlayingChanged;
				AudioPlayer.MediaFailed += AudioPlayer_MediaFailed;
				AudioPlayer.MediaFinished += AudioPlayer_MediaFinished;                              
				AudioPlayer.StatusChanged += AudioPlayer_StatusChanged;

				var url = Constants.RestUrl + "file/" + meditationFile.Path;
                                
				AudioPlayer.Play(new MediaFile(url, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, Plugin.MediaManager.Abstractions.Enums.ResourceAvailability.Remote));               
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
			Progress = e.Progress;
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
             
			var meditiondone = user.MeditationsDone?.Where((arg) => arg.id == SeanceModel.Meditation.Id).First();

			if(meditiondone!=null)
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

			await CoreMethods.PopPageModel(animate:false);
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

	}
}
