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
			et_email.NextView = et_pass;

			if(isSignUp)
			{
				
			}
			else
			{
				
			}
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
