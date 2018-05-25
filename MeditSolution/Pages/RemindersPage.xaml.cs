using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class RemindersPage : BasePage
    {
        public RemindersPage()
		{
            InitializeComponent();
        }

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			list.SelectedItem = null;
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			
		}


		void Handle_Clicked(object sender, System.EventArgs e)// button clicked
		{
			var bt = sender as Button;

			if(bt.BackgroundColor == Color.White)
			{
				bt.BackgroundColor = (Color)Application.Current.Resources["primary"];
				bt.TextColor = Color.White;
				bt.BorderColor = (Color)Application.Current.Resources["primary"];               
			}
			else
			{
				bt.BackgroundColor = (Color.White);
				bt.TextColor = Color.FromHex("9b9b9b");
				bt.BorderColor = Color.FromHex("9b9b9b");  
			}
		}
	}
}
