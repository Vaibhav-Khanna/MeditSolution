using System;
using System.Collections.ObjectModel;
using Syncfusion.SfPicker.XForms;
using Xamarin.Forms;
using System.Collections.Generic;
using MeditSolution.Resources;
using System.Linq;
using System.Threading.Tasks;

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

            //this.SelectionChanged += Handle_SelectionChanged;
   
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

        private void PopulateTimeCollection(bool isMaxLimit = false)
        {
   
            for (int i = 1; i <= 1000; i++)
            {
                Minute.Add(i.ToString());
            }

            if (isMaxLimit)
                Second.Add("00");
            else
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

        void Handle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newValue = e.NewValue as IEnumerable<object>;

            var oldValue = e.OldValue as IEnumerable<object>;

            if (oldValue == null)
                return;

            var new_min = Convert.ToInt32(newValue.First());
            var new_sec = Convert.ToInt32(newValue.Last());

            var old_min = Convert.ToInt32(oldValue.First());
            var old_sec = Convert.ToInt32(oldValue.Last());

            if (new_min == 12 && old_min != 12)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    this.SelectedItem = new ObservableCollection<object> { "12", "00" };
                    return;
                }

                Time = new ObservableCollection<object>();

                Minute = new ObservableCollection<object>();

                Second = new ObservableCollection<object>();

                PopulateTimeCollection(true);

                this.ItemsSource = Time;

                if (Device.RuntimePlatform == Device.Android)
                    this.SelectedItem = new ObservableCollection<object> { "12", "00" };
            }
            else if (old_min == 12 && new_min != 12)
            {
                Time = new ObservableCollection<object>();

                Minute = new ObservableCollection<object>();

                Second = new ObservableCollection<object>();

                PopulateTimeCollection();

                this.ItemsSource = Time;
            }
            else if (new_min == 12 && Device.RuntimePlatform == Device.iOS)
            {
                this.SelectedItem = new ObservableCollection<object> { "12", "00" };
            }
        }

    }
}
