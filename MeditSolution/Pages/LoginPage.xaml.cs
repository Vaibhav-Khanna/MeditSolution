using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using MeditSolution.PageModels;

namespace MeditSolution.Pages
{
    public partial class LoginPage : BasePage
    {

        public LoginPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);          
        }

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext is LoginPageModel)
			{
				attachNextButtons(((LoginPageModel)BindingContext).IsSignUp);
			}
		}

		void attachNextButtons(bool isSignUp)
		{
			et_email.ReturnButton = Controls.ReturnButtonType.Next;
			et_email.NextView = et_pass;

			if(isSignUp)
			{
				et_pass.ReturnButton = Controls.ReturnButtonType.Next;
				et_conpass.ReturnButton = Controls.ReturnButtonType.Done;

				et_pass.NextView = et_conpass;
				et_conpass.DoneView = bt;               
			}
			else
			{
				et_pass.ReturnButton = Controls.ReturnButtonType.Done;
				et_pass.DoneView = bt;
			}
		}

		protected override bool OnBackButtonPressed()
		{
			(BindingContext as LoginPageModel).CloseCommand.Execute(null);
			return true;
		}
	}
}
