using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.DataObjects;
using Newtonsoft.Json;

namespace MeditSolution.DataStore.Implementation.Stores
{
	public class ProgramStore : BaseStore<Program>, IProgramStore
	{
		public async Task<User> PayForNewProgram(string id, PaidProgram item)
		{

			var uri = new Uri(string.Format(Constants.RestUrl + Type + "/" + id + "/pay" + "?" + Auth));
			try
			{
				var json = JsonConvert.SerializeObject(item);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await client.PostAsync(uri, content);

				if (response.IsSuccessStatusCode)
				{
					var content2 = await response.Content.ReadAsStringAsync();
					var resp = JsonConvert.DeserializeObject<User>(content2);
					return resp;
				}
			}
			catch (Exception)
			{
			}

			return null;
		}
	}
}
