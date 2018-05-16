﻿using System;
using System.Collections.ObjectModel;
using Syncfusion.SfPicker.XForms;
using Xamarin.Forms;

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
				Headers.Add("MINUTE");

				Headers.Add("SECOND");
    
			}
			else
			{
				Headers.Add("Minute");

				Headers.Add("Second");               
			}
   
			HeaderText = "PICK DURATION";
			ShowColumnHeader = true;
			ShowFooter = true;

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
