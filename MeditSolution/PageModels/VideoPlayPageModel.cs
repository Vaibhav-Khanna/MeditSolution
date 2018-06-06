using System;
using Xamarin.Forms;
using MeditSolution.Controls;
using MeditSolution.Models;
using MeditSolution.Helpers;

namespace MeditSolution.PageModels
{
	public class VideoPlayPageModel : BasePageModel
    {
		
		public string Video { get; set; }
		public VideoModel Model { get; set; }
		bool IsLauncVideo;

		public async override void Init(object initData)
		{
			base.Init(initData);

			if(initData is Tuple<bool,VideoModel>)
			{
				IsLauncVideo = true;
				Model = ((Tuple<bool, VideoModel>)initData).Item2;
                Video = Constants.RestUrl + "file/" + (Settings.DeviceLanguage == "English" ? Model.Video.Path_EN : Model.Video.Path);	
			}
			else if (initData is VideoModel)
			{
				Model = ((VideoModel)initData);
				Video = Constants.RestUrl + "file/" + (Settings.DeviceLanguage == "English" ? Model.Video.Path_EN : Model.Video.Path);
			}
			else
			{
				IsLauncVideo = true;
				var videoData = await StoreManager.VideoStore.GetWelcomeVideo();
				Video = Constants.RestUrl + "file/" + (Settings.DeviceLanguage == "English" ? videoData.Path_EN : videoData.Path);
			}
		}
        
        
		public Command CloseCommand => new Command(async(obj) =>
		{
			if (IsLauncVideo)
            {
				Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = TabNavigator.GenerateTabPage();
                });
            }
			else
			{
				await CoreMethods.PopPageModel(true);
			} 
		});

	}
}
