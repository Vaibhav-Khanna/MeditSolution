using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeditSolution.DataStore.Abstraction;
using MeditSolution.Helpers;
using MeditSolution.Responses;
using Newtonsoft.Json;
using Plugin.Connectivity.Abstractions;
using Akavache;
using System.Reactive.Linq;
using System.Linq;

namespace MeditSolution.DataStore.Implementation
{
	public class BaseStore<T> : IBaseStore<T> where T : BaseDataObject
    {
		protected readonly HttpClient client;
		protected readonly string Type;
		protected string Auth => string.Concat("token=", Settings.Token);
        protected IBlobCache Cache => BlobCache.UserAccount;
        protected IConnectivity Connectivity => Plugin.Connectivity.CrossConnectivity.Current;

        public BaseStore()
        {
			client = new HttpClient();
			Type = typeof(T).Name.ToLower() + "s";
        }

		public async Task<T> GetItemAsync(string id)
		{
			try
			{
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
                {
                    var uri = new Uri(Constants.RestUrl + Type + "/" + id + "?" + Auth);
                    var response = await client.GetAsync(uri);
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<T>(content);
                        return result;
                    }
                }
                else
                {
                    var obj = await Cache.GetObject<T>(id);
                    return obj;
                }
			}
			catch (Exception)
			{

			}

			return null;
		}

		public async Task<IEnumerable<T>> GetItemsAsync()
		{
			try
			{
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
                {

                    var parameters = "?limit=" + 50 + "&page=" + 0;

                    var uri = new Uri(Constants.RestUrl + Type + parameters + "&" + Auth);

                    var response = await client.GetAsync(uri);
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var items = JsonConvert.DeserializeObject<PaginationResponse<T>>(content);

                        if(items!=null && items.rows!=null && items.rows.Any())
                        {
                            var diction = new Dictionary<string, T>();

                            foreach (var item in items.rows)
                            {
                                diction.Add(item.Id, item);
                            }

                            await Cache.InsertAllObjects<T>(diction);
                        }

                        return items.rows;
                    }
                }
                else
                {
                    return await Cache.GetAllObjects<T>();
                }
			}
			catch (Exception)
			{

			}

			return null;
		}

		public async Task<T> InsertAsync(T item)
		{
			var uri = new Uri(Constants.RestUrl + Type + "?" + Auth);
                
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
				var response = await client.PostAsync(uri, content);
               
                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<T>(content2);                 
                }					
            }
			catch (Exception)
            { 
				
            }

			return null;
		}

		public async Task<bool> RemoveAsync(T item)
		{
			var uri = new Uri(Constants.RestUrl + Type + "/" + item.Id + "?" + Auth);

            try
            {
				var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
					return true;
                }
            }
            catch (Exception)
            {      
                
            }

			return false;
		}

		public async Task<T> UpdateAsync(T item)
		{
			var uri = new Uri(Constants.RestUrl + Type + "/" + item.Id + "?" + Auth);

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content2);
                }
            }
            catch (Exception)
            {
				
            }

            return null;
		}
       
	}
}
