using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MeditationBreathePlayPageModel : BasePageModel
    {

		public string HeaderText { get; set; } = "Cohérence cardiaque";
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
