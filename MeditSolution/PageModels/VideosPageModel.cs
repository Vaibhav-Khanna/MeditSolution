using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class VideosPageModel : BasePageModel
    {
		public ObservableCollection<VideoModel> Videos { get; set; }


		public override void Init(object initData)
		{
			base.Init(initData);


			Videos = new ObservableCollection<VideoModel>();
			Videos.Add(new VideoModel() { Title = "Comment meditater", Duration = "3:34" });
			Videos.Add(new VideoModel() { Title = "Bienavenue", Duration = "2:10" });
			Videos.Add(new VideoModel() { Title = "Puorquoi mediter", Duration = "5:45" });
		}

		public Command PlayCommand => new Command(async(obj) =>
		{
			await CoreMethods.PushPageModel<VideoPlayPageModel>(obj);
		});

	}
}
