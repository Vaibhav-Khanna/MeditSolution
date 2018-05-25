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
			var bottomBarPage = new CustomNavigation() { BarBackgroundColor = Color.White };

			bottomBarPage.FixedMode = true;
			bottomBarPage.BarBackgroundColor = Color.White;
			bottomBarPage.BarTheme = BottomBarPage.BarThemeTypes.Light;

			if (Device.RuntimePlatform == Device.Android)
				bottomBarPage.BarTextColor = (Color)Application.Current.Resources["primaryDark"];

			bottomBarPage.AddTab<MeditationTabPageModel>("Méditation", "meditation.png");
			bottomBarPage.AddTab<CatalogueTabPageModel>("Catalogue", "catalogue.png");
			bottomBarPage.AddTab<StatsTabPageModel>("Mes stats", "stats.png");
			var container = bottomBarPage.AddTab<PlusTabPageModel>("Plus", "plus.png");
   
			return new DynamicNavigationPage(bottomBarPage);
		}

    }
}
