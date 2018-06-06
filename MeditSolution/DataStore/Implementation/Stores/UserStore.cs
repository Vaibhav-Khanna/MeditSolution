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
		public User User { get { return getUser(); } }

		User getUser()
		{
			if(!string.IsNullOrEmpty(Settings.User))
				return JsonConvert.DeserializeObject<User>(Settings.User);
                  
			return null;
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

		public async Task<User> UpdateCurrentUser(User user)
		{
			if (user == null)
				user = JsonConvert.DeserializeObject<User>(Settings.User);

			if (user != null)
            {
                Settings.User = JsonConvert.SerializeObject(user);
            }

			var _user = await UpdateAsync(user);

			if(_user!=null)
			{
				Settings.User = JsonConvert.SerializeObject(_user);
			}

			return _user;
		}
	}
}
