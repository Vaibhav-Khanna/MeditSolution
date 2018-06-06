using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MeditSolution.Helpers;
using MeditSolution.Models.Abstract;
using MeditSolution.Resources;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
    public class ForgotPasswordPageModel : BasePageModel
    {
        public string ButtonText { get; set; }
        public string ButtonColor { get; set; } = "#d9d9d9";
        public string Title { get; set; }

        string email;
        [PropertyChanged.DoNotNotify]
        public string Email { get { return email; } set { email = value; ValidateEmail(); RaisePropertyChanged(); } }

        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);
        });


        public override void Init(object initData)
        {
            base.Init(initData);

            Title = AppResources.forgot;
            ButtonText = AppResources.forgotButton;

            if (initData != null)
            {
                Email = (string)initData;
            }
        }

        private bool ValidateEmail()
        {
            bool isEmail;

            if (!string.IsNullOrWhiteSpace(Email))
            {
                isEmail = Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                
				if (isEmail == true)
                {
                    ButtonColor = "#50e3c2";
                }
                else
                {
                    ButtonColor = "#d9d9d9";
                }

            }
            else
            {
                isEmail = false;
                ButtonColor = "#d9d9d9";
            }
            return isEmail;
        }


        public ICommand SendEmailCommand
        {
            get
            {
                return new Command(async () =>
                {
					if (!ValidateEmail())
						return;

                    Dialog.ShowLoading("");
                    
					var Forgot = new ForgotPasswordRequest();
                    Forgot.email = Email;

                    var result = await StoreManager.RequestNewPassword(Forgot);

                    if (result)
                    {
                        Dialog.HideLoading();
                        await CoreMethods.PopPageModel(null, true);

                        await ToastService.Show(AppResources.sendnewpassword);
                    }
                    else
                    {
                        Dialog.HideLoading();
                        await CoreMethods.DisplayAlert(AppResources.wrongsendnewpassword, "", AppResources.ok);
                    }
                });
            }
        }

    }
}
