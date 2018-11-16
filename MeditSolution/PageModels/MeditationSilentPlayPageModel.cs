using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models;
using MeditSolution.Helpers;
using System.Linq;
using MeditSolution.Models.DataObjects;
using MeditSolution.Service;
using System.Diagnostics;

namespace MeditSolution.PageModels
{
    public class MeditationSilentPlayPageModel : BasePageModel
    {
        public double Progress { get; set; }

        private MeditationTimer _timer;

        public string TimerText { get; set; }

        public TimeSpan TotalSeconds { get; set; }

        private double step;

        SeancesModel SeancesModel;

        int TotalTimeMedited;

        public Color Tint { get; set; } = Color.FromHex("91f0db");

        public Color TintDark { get; set; } = Color.FromHex("45d1b2");


        public override void Init(object initData)
        {
            base.Init(initData);

            int durationInSeconds = 0;


            if (initData is SeancesModel)
            {

                SeancesModel = ((SeancesModel)initData);
                durationInSeconds = (int)SeancesModel.Meditation.Length;

                Tint = Color.FromHex(SeancesModel.Tint.Substring(1));
                TintDark = ((Tint).AddLuminosity(-0.2));
            }
            else
            {
                durationInSeconds = int.Parse(initData.ToString());
            }

            TotalTimeMedited = durationInSeconds;

            TotalSeconds = TimeSpan.FromSeconds(durationInSeconds);

            _timer = new MeditationTimer(TimeSpan.FromSeconds(1), CountDown);
            Progress = 0;
            step = (1 / TotalSeconds.TotalSeconds);
            _timer.Start();
        }


        private void CountDown()
        {
            if (TotalSeconds.TotalSeconds == 0)
            {
                TimerText = "00:00:00";

                TotalSeconds = new TimeSpan(0, 0, 0, 0);

                _timer.Stop();

                EndMeditation();
            }
            else
            {
                TotalSeconds = TotalSeconds.Subtract(new TimeSpan(0, 0, 0, 1));

                TimerText = TotalSeconds.ToString("hh':'mm':'ss");

                //  Debug.WriteLine(TimerText);

                Progress = Progress + step;
            }
        }

        public Command PlayPauseCommand => new Command(() =>
        {
            if (IsPlaying) { _timer.Stop(); }
            else { _timer.Start(); }
            IsPlaying = !IsPlaying;
        });

        public Command CloseCommand => new Command(async () =>
        {

            await CoreMethods.PopPageModel(animate: false);
        });

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);

            _timer?.Stop();
        }

        async void EndMeditation()
        {
            try
            {

                DependencyService.Get<IPlayNotificationSound>()?.PlaySound();

                Dialog.ShowLoading();

                var OriginalUser = StoreManager.UserStore.User;

                var isAdded = await StoreManager.MeditationStore.AddMeditationTimeAsync(TotalTimeMedited);

                var user = await StoreManager.UserStore.UpdateCurrentUser(StoreManager.UserStore.User);


                bool isFirstTime = false;

                if (SeancesModel != null)
                {
                    var meditiondone = user.MeditationsDone?.Where((arg) => arg.id == SeancesModel.Meditation.Id).First();

                    if (meditiondone != null)
                    {
                        var count = GetSeanceCount(SeancesModel.Meditation);

                        if (count == 1)
                        {
                            if (meditiondone.level2Done == false) { isFirstTime = true; }
                            meditiondone.level2Done = true;
                        }
                        else if (count == 2)
                        {
                            if (meditiondone.level3Done == false) { isFirstTime = true; }
                            meditiondone.level3Done = true;

                        }

                        else if (count == 3)
                        {
                            if (meditiondone.level4Done == false) { isFirstTime = true; }
                            meditiondone.level4Done = true;
                        }
                    }

                    user = await StoreManager.UserStore.UpdateCurrentUser(user);
                }

                Dialog.HideLoading();


                if (OriginalUser.CurrentLevel < user.CurrentLevel)
                {
                    await CoreMethods.PushPageModel<AvatarGrowPageModel>(null, modal: true);
                }

                if ((SeancesModel != null) && isFirstTime)
                {

                    await CoreMethods.PushPageModel<MeditationEndPageModel>(true, modal: true);
                    CoreMethods.RemoveFromNavigation<MeditationSilentPlayPageModel>(true);
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
