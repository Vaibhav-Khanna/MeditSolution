using System;
using Xamarin.Forms;
using MeditSolution.Helpers;
using System.Text.RegularExpressions;
using MeditSolution.Models.DataObjects;

namespace MeditSolution.PageModels
{
	public class MyAccountModifyPageModel : BasePageModel
    {
		public string Name { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public override void Init(object initData)
		{
			base.Init(initData);

			if (initData is User)
			{
				Email = ((User)initData).Email;
				Name = ((User)initData).Firstname;	
				LastName = ((User)initData).Lastname;
			}
		}

		public Command SaveCommand => new Command(async () =>
        {
			if(!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Email) && isEmail() && Password == ConfirmPassword)
			{
				Dialog.ShowLoading();

				var user = StoreManager.UserStore.User;

				user.Firstname = Name;
				user.Lastname = LastName;
				user.Email = Email;

				if(!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPassword))
				user.Password = Password;
                    
				await StoreManager.UserStore.UpdateCurrentUser(user);

				Dialog.HideLoading();

				await CoreMethods.PopPageModel();
			}
			else
			{
				await ToastService.Show("Please fill all the fields");
			}
        });
           

		bool isEmail()
		{
			if (!string.IsNullOrWhiteSpace(Email))
				return Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
			else
				return false;
		}

    }
}
