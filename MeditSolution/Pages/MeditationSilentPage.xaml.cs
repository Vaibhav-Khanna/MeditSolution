using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class MeditationSilentPage : BasePage
    {
        public MeditationSilentPage()
        {
            InitializeComponent();
            
        }

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			picker.IsOpen = true;
		}
    }
}
