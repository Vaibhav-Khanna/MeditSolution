using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MeditSolution.DataStore.Abstraction
{
	public interface IBaseStore<T> where T : IBaseDataObject
    {
		Task<IEnumerable<T>> GetItemsAsync();

		Task<T> GetItemAsync(string id);

		Task<T> InsertAsync(T item);

		Task<T> UpdateAsync(T item);

		Task<bool> RemoveAsync(T item); 
    }
}
