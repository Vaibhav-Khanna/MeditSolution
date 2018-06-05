using System;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;
using Newtonsoft.Json;

namespace MeditSolution.DataStore.Implementation.Stores
{
	public class UserStore : BaseStore<User>, IUserStore
	{
		public User User { get { return getUser(); } set { setUser(value); } }

		User getUser()
		{
			if(!string.IsNullOrEmpty(Settings.User))
				return JsonConvert.DeserializeObject<User>(Settings.User);
                  

			return null;
		}

		void setUser(User user)
		{
			if(user!=null)
			{
				Settings.User = JsonConvert.SerializeObject(user);
			}
		}

		public async Task<User> GetCurrentUser()
		{
			var item = await GetItemAsync("me");

			if (item != null)
			{
				Settings.User = JsonConvert.SerializeObject(item);
			}

			return item;
		}

		public async Task<User> UpdateCurrentUser()
		{
			if (string.IsNullOrEmpty(Settings.User))
				return null;

			var user = await UpdateAsync(getUser());

			if(user!=null)
			{
				Settings.User = JsonConvert.SerializeObject(user);
			}

			return user;
		}
	}
}
