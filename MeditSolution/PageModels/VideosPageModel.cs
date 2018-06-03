using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;
using System.Linq;
using MeditSolution.Helpers;

namespace MeditSolution.PageModels
{
	public class VideosPageModel : BasePageModel
    {
		public ObservableCollection<VideoModel> Videos { get; set; }
        
		public async override void Init(object initData)
		{
			base.Init(initData);

			IsLoading = true;

			var items = await StoreManager.VideoStore.GetItemsAsync();

			IsLoading = false;

			if (items != null && items.Any())
			{
				Videos = new ObservableCollection<VideoModel>();

				foreach (var item in items)
				{
					Videos.Add(new VideoModel(item));
				}
			}
			else
			{
				await ToastService.Show("Request failed");
			}
		}

		public Command PlayCommand => new Command(async(obj) =>
		{
			await CoreMethods.PushPageModel<VideoPlayPageModel>(obj,true);
		});

	}
}
