using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.DataObjects;
using MeditSolution.Responses;
using Newtonsoft.Json;

namespace MeditSolution.DataStore.Implementation.Stores
{
	public class MeditationStore : BaseStore<Meditation>, IMeditationStore
	{
		
		public async Task<bool> AddMeditationTimeAsync(int seconds)
		{
			var uri = new Uri(string.Concat(Constants.RestUrl + "users/store/meditation?", Auth));

			var json = JsonConvert.SerializeObject(new Dictionary<string, int> { { "time", seconds } });

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(uri, content);

			if (response.IsSuccessStatusCode)
			{
				var R_content = await response.Content.ReadAsStringAsync();
				return true;
			}

			return false;
		}

		public async Task<IEnumerable<Meditation>> GetMeditationsByProgramId(string id)
		{
			
			var parameters = "?limit=" + 100 + "&page=" + 0 + "&programId=" + id; 

			parameters += "&" + Auth;            

			var uri = new Uri(string.Format(Constants.RestUrl + Type + parameters, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
               
				if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<PaginationResponse<Meditation>>(content);
					return result.rows;
                }
            }
            catch (Exception)
            {     				
            }

			return null;
		}

		public async Task<NextMeditationResponse.Example> GetNextMeditation()
		{
			var uri = new Uri(string.Format(Constants.RestUrl + "next?" + Auth, string.Empty));

			var response = await client.GetAsync(uri);

			var content = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var meditationresponse = JsonConvert.DeserializeObject<NextMeditationResponse.Example>(content);
				return meditationresponse;
			}

			return null;
		}

	}
}
