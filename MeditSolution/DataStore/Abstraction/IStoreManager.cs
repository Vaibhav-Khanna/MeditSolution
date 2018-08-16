using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MeditSolution.DataStore.Abstraction.Stores;
using MeditSolution.Models.Abstract;
using MeditSolution.Models.DataObjects;

namespace MeditSolution.DataStore.Abstraction
{
    public interface IStoreManager
    {
        IMeditationStore MeditationStore { get; }
        IProgramStore ProgramStore { get; }
        IVideoStore VideoStore { get; }
        IUserStore UserStore { get; }

        Task<object> LoginAsync(string email, string password);
        void LogoutAsync();
        Task<object> RegisterAsync(string email, string password,string deviceLanguage);
        Task<bool> RequestNewPassword(ForgotPasswordRequest request);
        Task<bool> RegenerateToken();

        Task<User> SubscribeToPremium(string id, SubscriptionPremium item);
        Task<User> UnSubscribeToPremium(string id);
        Task<byte[]> GetUserAvatar();
    }
}
