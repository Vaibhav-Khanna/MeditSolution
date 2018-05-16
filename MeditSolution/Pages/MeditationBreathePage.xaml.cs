using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class MeditationBreathePage : BasePage
    {
        public MeditationBreathePage()
        {
            InitializeComponent();
        }

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			picker.IsOpen = true;
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{

			var bt_send = (sender as Button);

			(BindingContext as MeditationBreathePageModel).QuickTimeCommand.Execute(bt_send.Text);

			bt1.TextColor = (Color)App.Current.Resources["textLight"];
			bt2.TextColor = (Color)App.Current.Resources["textLight"];
			bt3.TextColor = (Color)App.Current.Resources["textLight"];
			bt4.TextColor = (Color)App.Current.Resources["textLight"];
			bt5.TextColor = (Color)App.Current.Resources["textLight"];

			bt1.BorderColor = (Color)App.Current.Resources["textLight"];
			bt2.BorderColor = (Color)App.Current.Resources["textLight"];
			bt3.BorderColor = (Color)App.Current.Resources["textLight"];
			bt4.BorderColor = (Color)App.Current.Resources["textLight"];
			bt5.BorderColor = (Color)App.Current.Resources["textLight"];

			bt1.BackgroundColor = (Color)App.Current.Resources["white"];
			bt2.BackgroundColor = (Color)App.Current.Resources["white"];
			bt3.BackgroundColor = (Color)App.Current.Resources["white"];
			bt4.BackgroundColor = (Color)App.Current.Resources["white"];
			bt5.BackgroundColor = (Color)App.Current.Resources["white"];

			bt_send.BackgroundColor = (Color)App.Current.Resources["primary"];
			bt_send.BorderColor = (Color)App.Current.Resources["primary"];
			bt_send.TextColor = Color.White;

		}

    }
}
