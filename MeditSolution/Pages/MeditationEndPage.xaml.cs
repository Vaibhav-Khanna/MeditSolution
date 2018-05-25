using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
	public partial class MeditationEndPage : BasePage
    {
        public MeditationEndPage()
        {
			Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeComponent();
        }
    }
}
