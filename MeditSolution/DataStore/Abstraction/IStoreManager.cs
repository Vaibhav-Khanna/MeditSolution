using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MeditSolution.DataStore.Abstraction.Stores;

namespace MeditSolution.DataStore.Abstraction
{
    public interface IStoreManager
    {
	    IMeditationStore MeditationStore { get; }

		Task<object> LoginAsync();
		Task<object> RegisterAsync();
    }
}
