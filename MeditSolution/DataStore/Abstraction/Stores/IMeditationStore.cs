using System;
using MeditSolution.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;
using MeditSolution.Responses;

namespace MeditSolution.DataStore.Abstraction.Stores
{
	public interface IMeditationStore : IBaseStore<Meditation>
	{
		Task<IEnumerable<Meditation>> GetMeditationsByProgramId(string id);

		Task<bool> AddMeditationTimeAsync(int seconds);

		Task<NextMeditationResponse.Example> GetNextMeditation();
    }
}
