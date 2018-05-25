using System;
using Xamarin.Forms;
using MeditSolution.Controls;
using MeditSolution.Models;

namespace MeditSolution.PageModels
{
	public class VideoPlayPageModel : BasePageModel
    {
		
		public string Video { get; set; }
		public VideoModel Model { get; set; }

		public override void Init(object initData)
		{
			base.Init(initData);

			if (initData is VideoModel)
			{
				Model = ((VideoModel)initData);
				Video = "https://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_1mb.mp4";
			}
			else
			{
				Video = "https://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_1mb.mp4";
			}
           
		}
        
        
		public Command CloseCommand => new Command(async(obj) =>
		{
			if (Model != null)
			{
				await CoreMethods.PopPageModel();
			}
			else
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage = TabNavigator.GenerateTabPage();
				});
			}
		});

	}
}
