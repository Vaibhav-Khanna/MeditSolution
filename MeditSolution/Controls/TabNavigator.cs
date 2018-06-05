using System;
using BottomBar.XamarinForms;
using MeditSolution.PageModels;
using Xamarin.Forms;
using MeditSolution.Resources;

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

			bottomBarPage.AddTab<MeditationTabPageModel>(AppResources.tab1, "meditation.png");
			bottomBarPage.AddTab<CatalogueTabPageModel>(AppResources.tab2, "catalogue.png");
			bottomBarPage.AddTab<StatsTabPageModel>(AppResources.tab3, "stats.png");
			var container = bottomBarPage.AddTab<PlusTabPageModel>(AppResources.tab4, "plus.png");
   
			return new DynamicNavigationPage(bottomBarPage);
		}

    }
}
