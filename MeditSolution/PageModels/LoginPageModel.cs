using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
    public class LoginPageModel : BasePageModel
    {

        public string ButtonText { get; set; }
        public string ButtonColor { get; set; } = "#d9d9d9";
        public string Title { get; set; }
        public bool IsSignUp { get; set; }

        string email;
        [PropertyChanged.DoNotNotify]
        public string Email { get{ return email; } set { email = value; ValidateCredentials(); } }

        string password;
        [PropertyChanged.DoNotNotify]
        public string Password { get { return password; } set { password = value; ValidateCredentials(); } }

        string conpassword;
        [PropertyChanged.DoNotNotify]
        public string ConfirmPassword { get { return conpassword; } set { conpassword = value; ValidateCredentials(); } }

        public LoginPageModel()
        {
            App.PostSuccessFacebookAction = token =>
            {
                
            };
        }

		public override void Init(object initData)
		{
            base.Init(initData);

            if(initData is bool)
            {
                if(((bool)initData))
                {
                    // login
                    Title = "Se connecter";
                    ButtonText = "Se connecter";
                    IsSignUp = false;
                }
                else
                {
                    //signup
                    Title = "S’incrire";
                    ButtonText = "Créer un compte";
                    IsSignUp = true;
                }
            }
		}

        public Command LoginCommand => new Command(async() =>
        {
            if(ValidateCredentials())
            {
               
            }
           
			await CoreMethods.PopPageModel(true, Device.RuntimePlatform == Device.iOS);
            await CoreMethods.PushPageModel<ChatPageModel>();

        });


        bool ValidateCredentials()
        {
            bool isEmail;

            if (!string.IsNullOrWhiteSpace(Email))
                isEmail = Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            else
                isEmail = false;

            if(IsSignUp)
            {
                if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPassword) && Password == ConfirmPassword && isEmail)
                {
                    ButtonColor = "#50e3c2";
                    return true;
                }
                else
                    ButtonColor = "#d9d9d9";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Password) && isEmail)
                {
                    ButtonColor = "#50e3c2";
                    return true;
                }
                else
                    ButtonColor = "#d9d9d9";  
            }

            return false;
        }

		public Command CloseCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(true,Device.RuntimePlatform == Device.iOS);
        });

    }
}
