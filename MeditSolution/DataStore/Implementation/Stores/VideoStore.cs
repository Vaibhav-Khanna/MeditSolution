using System;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.DataObjects;
using System.Linq;

namespace MeditSolution.DataStore.Implementation.Stores
{
	public class VideoStore : BaseStore<Video>, IVideoStore
	{
		public async Task<Video> GetWelcomeVideo()
		{
			var items = await GetItemsAsync();

			if (items != null && items.Any())
			{
				if (items.Where((arg) => arg.Name_EN.ToLower() == "welcome").Any())
				{
					return items.Where((arg) => arg.Name_EN.ToLower() == "welcome").First();
				}
			}

			return null;
		}
	}
}
