using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class MeditationSilentPlayPage : BasePage
    {
        public MeditationSilentPlayPage()
        {
			Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
