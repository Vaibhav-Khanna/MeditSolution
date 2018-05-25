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
using Xamarin.Essentials;

namespace MeditSolution.DataStore.Implementation
{
	public class BaseStore<T> : IBaseStore<T> where T : BaseDataObject
    {
		private readonly HttpClient client;
        private readonly string Type;
        private static string Auth => string.Concat("token=", Settings.Token);

        public BaseStore()
        {
			client = new HttpClient();
			Type = typeof(T).Name;
        }

		public async Task<T> GetItemAsync(string id)
		{
			var uri = new Uri(Constants.RestUrl + Type + "/" + id + "?" + Auth);
			var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var result = JsonConvert.DeserializeObject<T>(content);
				return result;
			}
			else
				return null;
		}

		public async Task<IEnumerable<T>> GetItemsAsync()
		{
			var uri = new Uri(Constants.RestUrl + Type + "?" + Auth);

			var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var items = JsonConvert.DeserializeObject<PaginationResponse<T>>(content);
				return items.rows;
			}
			else
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
