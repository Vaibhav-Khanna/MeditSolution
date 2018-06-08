using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
	public partial class CatalogueTabPage : BasePage
    {
		public CatalogueTabPage()
		{
			if (Device.RuntimePlatform == Device.iOS)
				Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

			Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, " ");
			InitializeComponent();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}
    }
}
