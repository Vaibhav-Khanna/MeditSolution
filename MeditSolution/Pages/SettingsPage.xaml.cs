using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
	public partial class SettingsPage : BasePage
    {
        public SettingsPage()
        {
            InitializeComponent();

			Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
