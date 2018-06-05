﻿using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models;

namespace MeditSolution.PageModels
{
    public class MeditationBreathePlayPageModel : BasePageModel
    {

        public string HeaderText { get; set; } = AppResources.coherentbreathing;

        public int CycleDuration { get; set; }
        public int Duration { get; set; }

        private MeditationTimer _timer;

        private TimeSpan _totalSeconds;
        [PropertyChanged.DoNotNotify]
        public TimeSpan TotalSeconds
        {
            get { return _totalSeconds; }
            set { _totalSeconds = value; }

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CycleDuration = (initData as Tuple<int, int>).Item1;
            Duration = (initData as Tuple<int, int>).Item2;


            _totalSeconds = TimeSpan.FromSeconds(Duration);

            _timer = new MeditationTimer(TimeSpan.FromSeconds(1), CountDown);
            TotalSeconds = _totalSeconds;

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
