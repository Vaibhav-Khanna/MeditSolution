using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
    public partial class CatalogueTabPage : ContentPage
    {
        public CatalogueTabPage()
        {
			Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
