using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.DataObjects;
using MeditSolution.Responses;
using Newtonsoft.Json;
using System.Reactive.Linq;
using MeditSolution.Helpers;

namespace MeditSolution.DataStore.Implementation.Stores
{
	public class MeditationStore : BaseStore<Meditation>, IMeditationStore
	{
		
		public async Task<bool> AddMeditationTimeAsync(int seconds)
		{
			try
			{
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
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
                }
                else
                {
                    var offline_time = Settings.TimeSecondsOffline;
                    offline_time += seconds;
                    Settings.TimeSecondsOffline = offline_time;
                }
			}
			catch(Exception ex)
			{
				
			}

			return false;
		}

        public async Task<bool> DownloadMeditationFile(string url, string fileName)
        {
            try
            {
                // download
			
                var _client = new HttpClient();

                var response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();

                    if (bytes != null && bytes.Any())
                    {
                        var is_saved = await PCLStore.WriteFileAsync(bytes, ".mp3", fileName);

                        if (!string.IsNullOrEmpty(is_saved))
                            return true;
                    }
                }               
            }
            catch(Exception)
            {    
                
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
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
                {

                    var response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PaginationResponse<Meditation>>(content);

                        if (result != null && result.rows != null && result.rows.Any())
                        {
                            var diction = new Dictionary<string, Meditation>();

                            foreach (var item in result.rows)
                            {
                                diction.Add(item.Id, item);
                            }

                            await Cache.InsertAllObjects<Meditation>(diction);
                        }

                        return result.rows;
                    }
                }
                else
                {
                    var meditations = await Cache.GetAllObjects<Meditation>();

                    if(meditations!=null && meditations.Any())
                    {
                        return meditations.Where((arg) => arg.ProgramId == id);
                    }
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

			try
			{
				var response = await client.GetAsync(uri);

				var content = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var meditationresponse = JsonConvert.DeserializeObject<NextMeditationResponse.Example>(content);
					return meditationresponse;
				}
			}
			catch(Exception ex)
			{
				
			}

			return null;
		}

        public async Task<Tuple<bool,string>> IsAvailableOffline(string fileName)
        {
            try
            {
                var name = await PCLStore.CheckFileExists(".mp3", fileName);

                if(!string.IsNullOrEmpty(name))
                {
                    return new Tuple<bool, string>(true, name);
                }
            }
            catch(Exception)
            {
                
            }

            return new Tuple<bool,string>(false,"");
        }
    }
}
