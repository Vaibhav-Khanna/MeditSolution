using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models;
using MeditSolution.Helpers;

namespace MeditSolution.PageModels
{
    public class MeditationBreathePlayPageModel : BasePageModel
    {

        public string HeaderText { get; set; } = AppResources.coherentbreathing;

		public string TimerText { get; set; }

        public int CycleDuration { get; set; }
       
		public int Duration { get; set; }

        private MeditationTimer _timer;

        public TimeSpan TotalSeconds { get; set; }

		int TotalTimeMedited;

        public override void Init(object initData)
        {
            base.Init(initData);

            CycleDuration = (initData as Tuple<int, int>).Item1;
            Duration = (initData as Tuple<int, int>).Item2;

			TotalTimeMedited = Duration;

            TotalSeconds = TimeSpan.FromSeconds(Duration);

            _timer = new MeditationTimer(TimeSpan.FromSeconds(1), CountDown);
        }

        private void CountDown()
        {
            if (TotalSeconds.TotalSeconds == 0)
            {
                TotalSeconds = new TimeSpan(0, 0, 0, 0);
				TimerText = "00:00:00";
                _timer.Stop();
				EndMeditation();
            }
            else
            {
                TotalSeconds = TotalSeconds.Subtract(new TimeSpan(0, 0, 0, 1));
				TimerText = TotalSeconds.ToString("hh':'mm':'ss");
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
            await CoreMethods.PopPageModel(true);
        });


		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
			base.ViewIsDisappearing(sender, e);

			_timer?.Stop();
   
		}

		async void EndMeditation()
		{
			Dialog.ShowLoading();

			var isAdded = await StoreManager.MeditationStore.AddMeditationTimeAsync(TotalTimeMedited);

			var user = await StoreManager.UserStore.UpdateCurrentUser(null);

			Dialog.HideLoading();
            
			await CoreMethods.PopPageModel(true);
		}

    }
}
