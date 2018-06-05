using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models;

namespace MeditSolution.PageModels
{
    public class MeditationSilentPlayPageModel : BasePageModel
    {
        public double Progress { get; set; }

        private MeditationTimer _timer;

        private TimeSpan _totalSeconds;

        public TimeSpan TotalSeconds
        {
            get { return _totalSeconds; }
            set { _totalSeconds = value; }
        }
        private double step;


        public override void Init(object initData)
        {
            base.Init(initData);

            int durationInSeconds = int.Parse(initData.ToString());

            _totalSeconds = TimeSpan.FromSeconds(durationInSeconds);

            _timer = new MeditationTimer(TimeSpan.FromSeconds(1), CountDown);
            TotalSeconds = _totalSeconds;
            Progress = 0;
            step = (1 / TotalSeconds.TotalSeconds);
            _timer.Start();

        }

        private void CountDown()
        {
            if (_totalSeconds.TotalSeconds == 0)
            {
                TotalSeconds = new TimeSpan(0, 0, 0, 0);
                _timer.Stop();
            }
            else
            {
                TotalSeconds = _totalSeconds.Subtract(new TimeSpan(0, 0, 0, 1));
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
            await CoreMethods.PopPageModel(true);
        });

    }
}
