using System;
using Xamarin.Forms;
using MeditSolution.Helpers;

namespace MeditSolution.PageModels
{
	public class MyAccountModifyPageModel : BasePageModel
    {

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public Command SaveCommand => new Command(async () =>
        {
			if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPassword) && Password == ConfirmPassword)
			{
				Dialog.ShowLoading();

				var user = StoreManager.UserStore.User;
				user.Password = Password;

				await StoreManager.UserStore.UpdateCurrentUser();

				Dialog.HideLoading();

				await CoreMethods.PopPageModel();
			}
			else
			{
				
			}
        });
           
    }
}
