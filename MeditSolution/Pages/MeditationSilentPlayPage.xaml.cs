using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using MeditSolution.PageModels;

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

		protected override bool OnBackButtonPressed()
		{
			(BindingContext as MeditationSilentPlayPageModel).CloseCommand.Execute(null);
			return true;
		}
	}
}
