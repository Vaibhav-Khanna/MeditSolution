using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MeditationPlayPageModel : BasePageModel
    {
		public double Progress { get; set; }
		public string TimerText { get; set; } = "00:00";


		public Command PlayPauseCommand => new Command(async()=>
		{
			IsPlaying = !IsPlaying;
            
			await CoreMethods.PushPageModel<MeditationEndPageModel>(IsPlaying,true);
		});


		public override void Init(object initData)
		{
			base.Init(initData);

			Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                Progress += 0.01;
                TimerText = $"00:{Progress}";
                return Progress >= 1 ? false : true;
            });
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
            
		}
	}
}
