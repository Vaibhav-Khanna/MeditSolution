using System;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction;
using MeditSolution.DataStore.Abstraction.Stores;

namespace MeditSolution.DataStore.Implementation
{
	public class StoreManager : IStoreManager
	{
		public IMeditationStore MeditationStore => throw new NotImplementedException();


		public Task<object> LoginAsync()
		{
			throw new NotImplementedException();
		}

		public Task<object> RegisterAsync()
		{
			throw new NotImplementedException();
		}
	}
}
