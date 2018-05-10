using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class MeditationTabPage : BasePage
    {
        public MeditationTabPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
