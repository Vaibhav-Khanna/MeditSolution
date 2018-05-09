using System;
using BottomBar.XamarinForms;
using MeditSolution.PageModels;
using Xamarin.Forms;

namespace MeditSolution.Controls
{
    public class TabNavigator
    {
       
		public static Page GenerateTabPage()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarBackgroundColor = Color.White };

                tabs.AddTab<MeditationTabPageModel>("Méditation", "meditation.png");
                tabs.AddTab<CatalogueTabPageModel>("Catalogue", "catalogue.png");
                tabs.AddTab<StatsTabPageModel>("Mes stats", "stats.png");
                tabs.AddTab<PlusTabPageModel>("Plus", "plus.png");

				return tabs;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
				var bottomBarPage = new CustomNavigation() { BarTextColor = (Color)Application.Current.Resources["primary"], BarBackgroundColor = Color.White };

                bottomBarPage.FixedMode = true;
                bottomBarPage.BarBackgroundColor = Color.White;
                bottomBarPage.BarTheme = BottomBarPage.BarThemeTypes.Light;
				bottomBarPage.BarTextColor = (Color)Application.Current.Resources["primary"];

                bottomBarPage.AddTab<MeditationTabPageModel>("Méditation", "meditation.png");
                bottomBarPage.AddTab<CatalogueTabPageModel>("Catalogue", "catalogue.png");
                bottomBarPage.AddTab<StatsTabPageModel>("Mes stats", "stats.png");
                bottomBarPage.AddTab<PlusTabPageModel>("Plus", "plus.png");

				return bottomBarPage;
            }

			return null;
        }

    }
}
