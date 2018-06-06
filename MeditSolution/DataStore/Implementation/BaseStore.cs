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
		protected readonly HttpClient client;
		protected readonly string Type;
		protected string Auth;

        public BaseStore()
        {
			client = new HttpClient();
			Type = typeof(T).Name.ToLower() + "s";
			Auth = string.Concat("token=", Settings.Token);
        }

		public async Task<T> GetItemAsync(string id)
		{
			try
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
			catch (Exception)
			{

			}

			return null;
		}

		public async Task<IEnumerable<T>> GetItemsAsync()
		{
			try
			{
				var parameters = "?limit=" + 50 + "&page=" + 0;

				var uri = new Uri(Constants.RestUrl + Type + parameters + "&" + Auth);
                
				var response = await client.GetAsync(uri);
				var content = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var items = JsonConvert.DeserializeObject<PaginationResponse<T>>(content);
					return items.rows;
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
