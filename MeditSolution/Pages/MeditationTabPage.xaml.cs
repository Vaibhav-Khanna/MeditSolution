using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
	public partial class MeditationTabPage : BasePage
    {
        public MeditationTabPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();                  

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			list.SelectedItem = null;
		}
          
    }
}
