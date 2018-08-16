using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using MeditSolution.DataStore.Abstraction;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Helpers;
using MeditSolution.Models.Abstract;
using MeditSolution.Models.DataObjects;
using MeditSolution.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reactive.Linq;
using Akavache;

namespace MeditSolution.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
        IMeditationStore meditationStore;
        public IMeditationStore MeditationStore => meditationStore ?? (meditationStore = FreshIOC.Container.Resolve<IMeditationStore>());

        IProgramStore programStore;
        public IProgramStore ProgramStore => programStore ?? (programStore = FreshIOC.Container.Resolve<IProgramStore>());

        IVideoStore videoStore;
        public IVideoStore VideoStore => videoStore ?? (videoStore = FreshIOC.Container.Resolve<IVideoStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());


        public async Task<object> LoginAsync(string email, string password)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "login", string.Empty));

            try
            {
                var credentials = new JObject();
                credentials["email"] = email;
                credentials["password"] = password;

                var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<TokenResponse>(content2);

                    // Store token in settings
                    Settings.Token = resp.token;
                    Settings.TokenExpiration = resp.tokenExpiration;
                    Settings.IsLoggedIn = true;
                    //
                    return resp;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public async void LogoutAsync()
        {
            Settings.Token = string.Empty;
            Settings.User = string.Empty;
            Settings.IsLoggedIn = false;
            var language = Settings.Language;
            var voice = Settings.Voice;
            await BlobCache.UserAccount.InvalidateAll();
            await Plugin.Notifications.CrossNotifications.Current.CancelAll();

            Plugin.Settings.CrossSettings.Current.Clear();

            Settings.Language = language;
            Settings.Voice = voice;

            // Do not remove downloads on log out. As per client request.
            //await PCLStore.DeleteEverything();
        }

        public async Task<bool> RegenerateToken()
        {
            try
            {
                string Auth = string.Concat("token=", Settings.Token);

                var uri = new Uri(string.Format(Constants.RestUrl + "regenerate" + "?" + Auth));

                var client = new HttpClient();

                var response = await client.GetAsync(uri);

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resp = JsonConvert.DeserializeObject<TokenResponse>(content);
                    // Store token in settings
                    Settings.Token = resp.token;
                    Settings.TokenExpiration = resp.tokenExpiration;
                    //
                    return true;
                }
            }
            catch(Exception)
            {
                
            }

            return false;
        }

        public async Task<object> RegisterAsync(string email, string password,string deviceLanguage)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "register", string.Empty));

            var credentials = new JObject();
            credentials["email"] = email;
            credentials["password"] = password;
            credentials["language"] = deviceLanguage;

            var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Store token in settings
                Settings.Token = tokenResponse.token;
                Settings.TokenExpiration = tokenResponse.tokenExpiration;
                Settings.IsLoggedIn = true;
                //

                return tokenResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return "User already Exists";
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RequestNewPassword(ForgotPasswordRequest request)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "forgot", string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync(uri, content);

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

        public async Task<User> SubscribeToPremium(string id, SubscriptionPremium item)
        {
            string Auth = string.Concat("token=", Settings.Token);

            var uri = new Uri(string.Format(Constants.RestUrl + "users/" + id + "/subscribe" + "?" + Auth));
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();

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

        public async Task<User> UnSubscribeToPremium(string id)
        {
            string Auth = string.Concat("token=", Settings.Token);

            var uri = new Uri(string.Format(Constants.RestUrl + "users/" + id + "/unsubscribe" + "?" + Auth));

            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(uri);

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

        public async Task<byte[]> GetUserAvatar()
        {
            string Auth = string.Concat("token=", Settings.Token);

            var uri = new Uri(string.Format(Constants.RestUrl + "users/me/avatar?" + Auth, string.Empty));

            var client = new HttpClient();

            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
            {
                return ReadFully(content);
            }

            return null;
        }

        public static bool NeedsTokenRefresh()
        {
            if (Settings.TokenExpiration == 0)
                return true;

            if (new DateTime(1970, 1, 1).AddSeconds(Settings.TokenExpiration).CompareTo(DateTime.Now.AddHours(2)) <= 0)
                return true;

            return false;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
