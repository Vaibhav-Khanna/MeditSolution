using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MeditSolution.PageModels
{
    public class MeditationSilentPageModel : BasePageModel
    {
        
        object time = new ObservableCollection<object>{ "1", "00" };
        [PropertyChanged.DoNotNotify]
        public object SelectedTime
        {
            get { return time; }
            set
            {
                if (value != null)
                {
                    time = value;
                    Time = $"{(time as ObservableCollection<object>).First()}:{(time as ObservableCollection<object>).Last()}";
                }
            }
        }

        public string Time { get; set; } = "1:00";


        public Command StartCommand => new Command(async () =>
        {
            try
            {
                int minute = int.Parse((SelectedTime as ObservableCollection<object>).First().ToString());
                int second = int.Parse((SelectedTime as ObservableCollection<object>).Last().ToString());

                object SelectedTimeInSecond = ((minute * 60) + second).ToString();
                await CoreMethods.PushPageModel<MeditationSilentPlayPageModel>(SelectedTimeInSecond, true);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erreur", e);
            }
        });
    }
}
