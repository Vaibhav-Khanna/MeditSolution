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
        object time;
        [PropertyChanged.DoNotNotify]
        public object SelectedTime
        {
            get { return time; }
            set
            {
                time = value;
                if (time != null)
                    Time = $"{(time as ObservableCollection<object>).First()}:{(time as ObservableCollection<object>).Last()}";
            }
        }

        public string Time { get; set; } = "1:00";


        public Command StartCommand => new Command(async () =>
        {
            try
            {
                int minute = int.Parse((time as ObservableCollection<object>).First().ToString());
                int second = int.Parse((time as ObservableCollection<object>).Last().ToString());

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
