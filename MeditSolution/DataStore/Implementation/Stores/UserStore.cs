using System;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;
using Newtonsoft.Json;
using FreshMvvm;

namespace MeditSolution.DataStore.Implementation.Stores
{
	public class UserStore : BaseStore<User>, IUserStore
	{
		public User User { get { return getUser(); } }
        IMeditationStore MeditationStore = FreshIOC.Container.Resolve<IMeditationStore>();

		User getUser()
		{
			if(!string.IsNullOrEmpty(Settings.User))
				return JsonConvert.DeserializeObject<User>(Settings.User);
                  
			return null;
		}
               
		public async Task<User> GetCurrentUser()
		{
            if (await Connectivity.IsRemoteReachable("https://www.google.com"))
            {
                if(Settings.TimeSecondsOffline != 0)
                {
                    var isupdated = await MeditationStore.AddMeditationTimeAsync(Settings.TimeSecondsOffline);

                    if (isupdated)
                        Settings.TimeSecondsOffline = 0;
                }

                var item = await GetItemAsync("me");

                if (item != null)
                {
                    Settings.User = JsonConvert.SerializeObject(item);
                }

                return item;
            }
            else
            {
                var user = getUser();
                return user;
            }	
		}

		public async Task<User> UpdateCurrentUser(User user)
		{
			if (user == null)
				user = JsonConvert.DeserializeObject<User>(Settings.User);

			if (user != null)
            {
                Settings.User = JsonConvert.SerializeObject(user);
            }

            if (await Connectivity.IsRemoteReachable("https://www.google.com"))
            {
                if (Settings.TimeSecondsOffline != 0)
                {
                    var isupdated = await MeditationStore.AddMeditationTimeAsync(Settings.TimeSecondsOffline);

                    if (isupdated)
                        Settings.TimeSecondsOffline = 0;
                }

                // check the premium status change
                var item = await GetItemAsync("me");

                if (item != null)
                {
                    user.IsExplicitPremium = item.IsExplicitPremium;
                }
                //

                var _user = await UpdateAsync(user);

                if (_user != null)
                {
                    Settings.User = JsonConvert.SerializeObject(_user);
                }
                else
                    return user;

                return _user;
            }
            else
            {
                return user;
            }
		}
	}
}
