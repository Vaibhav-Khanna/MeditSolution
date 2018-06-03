using System;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
	public class MeditationBreathePlayPageModel : BasePageModel
    {

		public string HeaderText { get; set; } = AppResources.coherentbreathing;
		public string TimerText { get; set; } = "00:00";
		public int CycleDuration { get; set; } = 8;
		public int Duration { get; set; } = 1;

		public override void Init(object initData)
		{
			base.Init(initData);            
		}

		public Command PlayPauseCommand => new Command(() =>
		{
			IsPlaying = !IsPlaying;
		});

		public Command CloseCommand => new Command(async() =>
		{
			await CoreMethods.PopPageModel(true);
		});
    }
}
