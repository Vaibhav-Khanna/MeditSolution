using System;
using Xamarin.Forms;
using MeditSolution.Controls;

namespace MeditSolution.PageModels
{
	public class VideoPlayPageModel : BasePageModel
    {
		
		public string Video { get; set; }

		public override void Init(object initData)
		{
			base.Init(initData);

			Video = "https://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_1mb.mp4";
		}
        
        
		public Command CloseCommand => new Command((obj) =>
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				Application.Current.MainPage = TabNavigator.GenerateTabPage();
			});
		});

	}
}
