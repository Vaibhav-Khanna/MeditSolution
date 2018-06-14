using System;
using MeditSolution.PageModels;
using Xamarin.Forms;
using Plugin.Notifications;
using Newtonsoft.Json;

namespace MeditSolution.Models
{
    public class ReminderModel : BasePageModel
    {

        public ReminderModel()
        {
           
        }

        public int Id { get; set; }

        [JsonIgnore]
        public RemindersPageModel Model;

        TimeSpan _time;
        [PropertyChanged.DoNotNotify]
        public TimeSpan Time
        {
            get
            { return _time; }
            set
            {
                _time = value;

                string hours = _time.Hours < 10 ? "0"+_time.Hours : _time.Hours.ToString();
                string mins = _time.Minutes < 10 ? "0" + _time.Minutes : _time.Minutes.ToString();

                TimeString = hours + ":" + mins;
              
                RaisePropertyChanged();
            }
        }

        public string TimeString { get; set; } = "00:00";

        bool isactive = false;
        [PropertyChanged.DoNotNotify]
        public bool IsActive
        {
            get { return isactive; }
            set
            {
                isactive = value;
                RaisePropertyChanged();
            }
        }

        public ReminderDayModel Monday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Monday };
        public ReminderDayModel Tuesday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Tuesday };
        public ReminderDayModel Wednesday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Wednesday };
        public ReminderDayModel Thursday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Thursday };
        public ReminderDayModel Friday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Friday };
        public ReminderDayModel Saturday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Saturday };
        public ReminderDayModel Sunday { get; set; } = new ReminderDayModel() { Day = DayOfWeek.Sunday };


        [JsonIgnore]
        public Command DeleteCommand => new Command((obj) =>
        {
            Model?.DeleteCommand.Execute(obj);
        });
    }
}
