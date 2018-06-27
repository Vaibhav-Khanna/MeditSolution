using System;
using System.Collections.ObjectModel;
using Syncfusion.SfPicker.XForms;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.Controls
{
	public class CustomTimePicker : SfPicker
    {
		
        public ObservableCollection<object> Time { get; set; }
  
		public ObservableCollection<object> Second;
         
		public ObservableCollection<object> Minute;
  
        public ObservableCollection<string> Headers { get; set; }

		public CustomTimePicker()
		{ 
            
			Time = new ObservableCollection<object>();

			Minute = new ObservableCollection<object>();

			Second = new ObservableCollection<object>();

			PopulateTimeCollection();

			this.ItemsSource = Time;

			Headers = new ObservableCollection<string>();

			if (Device.RuntimePlatform == Device.Android)
			{
                Headers.Add(AppResources.minute.ToUpper());

                Headers.Add(AppResources.seconds.ToUpper());    
			}
			else
			{
                Headers.Add(AppResources.minute);

                Headers.Add(AppResources.seconds);              
			}
   
            HeaderText = AppResources.pickduration;
			ShowColumnHeader = true;
            ShowFooter = true;
            FooterHeight = 40;
            var lb = new Label() { Text = AppResources.ok, FontSize = 18, VerticalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, TextColor = (Color)App.Current.Resources["primary"], HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center };
            lb.GestureRecognizers.Add(new TapGestureRecognizer((obj) =>
            {
                IsOpen = false;
            }));
            FooterView = lb;

			HeaderTextColor = (Color)App.Current.Resources["primaryDark"];
			ColumnHeaderTextColor = (Color)App.Current.Resources["primary"];
                    
			this.ColumnHeaderText = Headers;
		}

        private void PopulateTimeCollection()
        {
   
            for (int i = 1; i <= 12; i++)
            {
                Minute.Add(i.ToString());
            }


			for (int j = 0; j < 60; j++)
			{
				if (j < 10)
				{
					Second.Add("0" + j);
				}
				else

					Second.Add(j.ToString());
			}

   
            Time.Add(Minute);

            Time.Add(Second); 
        }
    }
}
