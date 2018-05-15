using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MeditationPlayPageModel : BasePageModel
    {
		bool _playing = true;
		[PropertyChanged.DoNotNotify]
		public bool IsPlaying { get { return _playing;  } set { _playing = value; Image = (value) ? "pause.png" : "playbig.png"; RaisePropertyChanged(); } }
		public string Image { get; set; } = "pause.png";

		public double Progress { get; set; }
		public string TimerText { get; set; } = "00:00";

		public Command PlayPauseCommand => new Command(()=>
		{
			IsPlaying = !IsPlaying;
		});


		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			Device.StartTimer(new TimeSpan(0, 0, 1), () =>
			   {
				   Progress += 0.01;
				   TimerText = $"00:{Progress}";
				   return Progress >= 1 ? false : true;
			   });
		}
	}
}
