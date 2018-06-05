using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
    public partial class ForgotPasswordPage : BasePage
    {
        public ForgotPasswordPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
