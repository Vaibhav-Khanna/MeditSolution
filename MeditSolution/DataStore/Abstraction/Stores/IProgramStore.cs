using System;
using System.Threading.Tasks;
using MeditSolution.Models.DataObjects;

namespace MeditSolution.DataStore.Abstraction.Stores
{
	public interface IProgramStore : IBaseStore<Program>
    {
		Task<User> PayForNewProgram(string id, PaidProgram item);
    }
}
