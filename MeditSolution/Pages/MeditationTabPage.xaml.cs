using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;
using System.Linq;

namespace MeditSolution.Pages
{
	public partial class MeditationTabPage : BasePage
    {
        public MeditationTabPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();         
        }

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			list.SelectedItem = null;
		}


	}
}
