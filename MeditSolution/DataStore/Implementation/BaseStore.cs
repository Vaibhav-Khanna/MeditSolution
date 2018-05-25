using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction;

namespace MeditSolution.DataStore.Implementation
{
	public class BaseStore<T> : IBaseStore<T> where T : IBaseDataObject
    {
        public BaseStore()
        {
			
        }

		public Task<T> GetItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> InsertAsync(T item)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(T item)
		{
			throw new NotImplementedException();
		}

		public Task<T> UpdateAsync(T item)
		{
			throw new NotImplementedException();
		}
	}
}
