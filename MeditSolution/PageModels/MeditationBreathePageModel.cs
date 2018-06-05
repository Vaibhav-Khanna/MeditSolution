using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
    public class MeditationBreathePageModel : BasePageModel
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
                {
                    Time = $"{(time as ObservableCollection<object>).First()}:{(time as ObservableCollection<object>).Last()}";
                }
            }
        }

        public string Time { get; set; } = "1:00";

        public int CycleDuration { get; set; } = 6;


        public Command QuickTimeCommand => new Command((obj) =>
        {
            CycleDuration = Convert.ToInt32(obj as string);
        });

        public Command StartCommand => new Command(async () =>
        {

            try
            {
                int minute = int.Parse((time as ObservableCollection<object>).First().ToString());
                int second = int.Parse((time as ObservableCollection<object>).Last().ToString());

                int SelectedTimeInSecond = ((minute * 60) + second);

                var tuple = new Tuple<int, int>(CycleDuration, SelectedTimeInSecond);


                await CoreMethods.PushPageModel<MeditationBreathePlayPageModel>(tuple, true);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erreur", e);

            }
        });

    }
}
