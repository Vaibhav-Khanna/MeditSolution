using System;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
    public class MeditationSilentPlayPageModel : BasePageModel
    {
        public double Progress { get; set; }
        public string TimerText { get; set; }
        public string HeaderText { get; set; } = AppResources.silentmeditation;


        public override void Init(object initData)
        {
            base.Init(initData);

            int durationInSeconds = int.Parse(initData.ToString());

            TimeSpan time = TimeSpan.FromSeconds(durationInSeconds);


            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                Progress -= 0.01;
                // TimerText = $"00:{Progress}";

                TimerText = time.ToString(@"mm\:ss");

                time = time.Subtract(TimeSpan.FromSeconds(1));

                return Progress >= 1 ? false : true;
            });
        }

        public Command PlayPauseCommand => new Command(() =>
        {
            IsPlaying = !IsPlaying;
        });

        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);
        });
    }
}
