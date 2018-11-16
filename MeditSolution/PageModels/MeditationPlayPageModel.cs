using System;
using MeditSolution.Models;
using MeditSolution.Models.DataObjects;
using Xamarin.Forms;
using MeditSolution.Helpers;
using Plugin.MediaManager;
using System.Linq;
using MeditSolution.Resources;
using Plugin.MediaManager.Abstractions.Implementations;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions;
using System.Threading.Tasks;

namespace MeditSolution.PageModels
{
    public class MeditationPlayPageModel : BasePageModel
    {
        public double Progress { get; set; }
        public string TimerText { get; set; }
        public string Title { get; set; }
        public bool InBackground = false;
        public bool HasEnded = false;

        SeancesModel SeanceModel;
        IMediaManager AudioPlayer => CrossMediaManager.Current;
        public Color Tint { get; set; }
        public Color TintDark { get; set; }
        TimeSpan position;
        MediaFile file;

        public Command PlayPauseCommand => new Command(() =>
        {
            if (AudioPlayer.Status == MediaPlayerStatus.Playing)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await AudioPlayer.Pause();
                    position = AudioPlayer.Position;
                    //AudioPlayer_MediaFinished(null, null);
                });
            }
            else if (AudioPlayer.Status == MediaPlayerStatus.Paused)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await AudioPlayer.Play();
                });
            }
        });

        void App_ApplicationIsPaused(bool obj)
        {
            if (obj)
            {
                InBackground = true;
                // paused
            }
            else
            {
                InBackground = false;

                if (HasEnded)
                {
                    EndMeditation();
                }
                // resumed
            }
        }



        public async override void Init(object initData)
        {
            base.Init(initData);

            if (initData is SeancesModel)
            {
                IsLoading = true;

                SeanceModel = ((SeancesModel)initData);

                this.PageWasPopped += Handle_PageWasPopped;

                App.ApplicationIsPaused += App_ApplicationIsPaused;

                Tint = Color.FromHex(SeanceModel.Tint.Substring(1));
                TintDark = ((Tint).AddLuminosity(-0.2));
                Title = Settings.DeviceLanguage == "English" ? SeanceModel.Meditation.Label_EN : SeanceModel.Meditation.Label;

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
                    var data = await StoreManager.MeditationStore.IsAvailableOffline(SeanceModel.Meditation.Id + SeanceModel.Level + Settings.Voice + Settings.Language);

                    if (data != null && data.Item1 && data.Item2 != null)
                    {
                        var pathToFileURL = new System.Uri(data.Item2).AbsolutePath;

                        if (Device.RuntimePlatform == Device.Android)
                            Device.BeginInvokeOnMainThread(async () =>
                           {
                               await AudioPlayer.Play(file = new MediaFile(pathToFileURL, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, ResourceAvailability.Local));
                           });
                        else
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await AudioPlayer.Play(file = new MediaFile("file://" + pathToFileURL));
                            });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await AudioPlayer.Play(file = new MediaFile(url, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, Plugin.MediaManager.Abstractions.Enums.ResourceAvailability.Remote));
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await AudioPlayer.Play(file = new MediaFile(url, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, Plugin.MediaManager.Abstractions.Enums.ResourceAvailability.Remote));
                    });
                }
            }
        }

        void AudioPlayer_StatusChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.StatusChangedEventArgs e)
        {
            if (e.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Paused)
            {
                IsPlaying = false;
            }
            else if (e.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Playing)
            {
                IsLoading = false;
                IsPlaying = true;
            }
            else if (e.Status == MediaPlayerStatus.Stopped)
            {
                position = AudioPlayer.Position;
                IsPlaying = false;
            }
        }

        void AudioPlayer_PlayingChanged(object sender, Plugin.MediaManager.Abstractions.EventArguments.PlayingChangedEventArgs e)
        {
            Progress = Device.RuntimePlatform == Device.iOS ? e.Progress : e.Progress / 100;
            TimerText = e.Duration.Subtract(e.Position).ToString("hh':'mm':'ss");
        }

        void AudioPlayer_MediaFailed(object sender, Plugin.MediaManager.Abstractions.EventArguments.MediaFailedEventArgs e)
        {

        }

        async void AudioPlayer_MediaFinished(object sender, Plugin.MediaManager.Abstractions.EventArguments.MediaFinishedEventArgs e)
        {
            HasEnded = true;


            await AudioPlayer?.Stop();


            if (IsLoading || InBackground)
                return;

            if (Device.RuntimePlatform == Device.iOS && AudioPlayer.Position.Ticks != 0)
            {
                return;
            }

            IsLoading = true;

            EndMeditation();
        }

        async void EndMeditation()
        {
            try
            {

                Dialog.ShowLoading();

                var OriginalUser = StoreManager.UserStore.User;

                var isAdded = await StoreManager.MeditationStore.AddMeditationTimeAsync((int)SeanceModel.Meditation.Length);

                var user = await StoreManager.UserStore.UpdateCurrentUser(StoreManager.UserStore.User);

                //var user = StoreManager.UserStore.User;
                var meditiondone = user?.MeditationsDone?.Where((arg) => arg.id == SeanceModel?.Meditation.Id).First();
                bool isFirstTime = false;
                if (meditiondone != null)
                {
                    if (SeanceModel.Level == 1)
                    {
                        if (meditiondone.level1Done == false) { isFirstTime = true; }
                        meditiondone.level1Done = true;
                    }
                    else if (SeanceModel.Level == 2)
                    {
                        if (meditiondone.level2Done == false) { isFirstTime = true; }
                        meditiondone.level2Done = true;

                    }
                    else if (SeanceModel.Level == 3)
                    {
                        if (meditiondone.level3Done == false) { isFirstTime = true; }
                        meditiondone.level3Done = true;

                    }

                    user = await StoreManager.UserStore.UpdateCurrentUser(user);
                }


                Dialog.HideLoading();

                IsLoading = false;

                if (OriginalUser.CurrentLevel < user.CurrentLevel)
                {
                    await CoreMethods.PushPageModel<AvatarGrowPageModel>(null, modal: true);
                }
                if (isFirstTime)
                {
                    if (GetSeanceCount(SeanceModel.Meditation) == SeanceModel.Level)
                    {

                        await CoreMethods.PushPageModel<MeditationEndPageModel>(true, modal: true);

                    }
                    else
                    {
                        await CoreMethods.PushPageModel<MeditationEndPageModel>(SeanceModel.Meditation, modal: true);

                    }

                    Handle_PageWasPopped(null, null);
                    CoreMethods.RemoveFromNavigation<MeditationPlayPageModel>(true);
                }
                else
                {

                    await CoreMethods.PopPageModel(animate: false);
                }



            }
            catch (Exception ex)
            {

            }
        }

        void Handle_PageWasPopped(object sender, EventArgs e)
        {
            AudioPlayer?.Stop();
            App.ApplicationIsPaused -= App_ApplicationIsPaused;

            if (AudioPlayer != null)
            {
                AudioPlayer.PlayingChanged -= AudioPlayer_PlayingChanged;
                AudioPlayer.MediaFailed -= AudioPlayer_MediaFailed;
                AudioPlayer.MediaFinished -= AudioPlayer_MediaFinished;
                AudioPlayer.StatusChanged -= AudioPlayer_StatusChanged;
            }

            this.PageWasPopped -= Handle_PageWasPopped;

            if (Device.RuntimePlatform == Device.Android)
            {
                DefaultNavigationBackgroundColor();
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Device.RuntimePlatform == Device.Android)
            {
                ChangeNavigationBackgroundColor(Tint);
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

            if ((meditation.Level1FrWoman != null) || (meditation.Level1EnWoman != null) || (meditation.Level1FrMan != null) || (meditation.Level1EnMan != null))
                seanceCount += 1;
            if ((meditation.Level2FrWoman != null) || (meditation.Level2EnWoman != null) || (meditation.Level2FrMan != null) || (meditation.Level2EnMan != null))
                seanceCount += 1;
            if ((meditation.Level3FrWoman != null) || (meditation.Level3EnWoman != null) || (meditation.Level3FrMan != null) || (meditation.Level3EnMan != null))
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

            return Color.FromRgb((int)red, (int)green, (int)blue);
        }
    }
}
