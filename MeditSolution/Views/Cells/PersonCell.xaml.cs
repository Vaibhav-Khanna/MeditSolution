using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MeditSolution.Views.Cells
{
    public partial class PersonCell : ViewCell
    {
        public PersonCell()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (!bt_no.IsEnabled)
                return;
            
            bt_yes.BackgroundColor = (Color)App.Current.Resources["primary"];
            bt_yes.TextColor = Color.White;

            bt_no.FadeTo(0);
			await bt_yes.TranslateTo(((bt_no.X + bt_no.Width) - bt_yes.Width) - bt_yes.X,0);

            bt_no.IsEnabled = false;
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            if (!bt_yes.IsEnabled)
                return;

            bt_no.BackgroundColor = (Color)App.Current.Resources["primary"];
            bt_no.TextColor = Color.White;

            await bt_yes.FadeTo(0);
           
            bt_yes.IsEnabled = false;
        }
    }
}
