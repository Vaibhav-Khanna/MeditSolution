using System;
using MeditSolution.Models.DataObjects;
using System.Threading.Tasks;

namespace MeditSolution.DataStore.Abstraction.Stores
{
	public interface IVideoStore : IBaseStore<Video>
    {
		Task<Video> GetWelcomeVideo();
    }
}
