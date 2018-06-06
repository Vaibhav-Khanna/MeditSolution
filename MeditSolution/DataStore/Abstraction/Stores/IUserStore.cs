using System;
using MeditSolution.Models.DataObjects;
using System.Threading.Tasks;

namespace MeditSolution.DataStore.Abstraction.Stores
{
	public interface IUserStore : IBaseStore<User>
    {
		User User { get; }

		Task<User> GetCurrentUser();

		Task<User> UpdateCurrentUser(User user);
    }
}
