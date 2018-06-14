using System;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace MeditSolution.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ReminderDayModel
    {
        public ReminderDayModel()
        {
        }

        public DayOfWeek Day { get; set; }

        public string DisplayText { get { return Day.ToString().Substring(0, 1); } }

        public bool IsEnabled { get; set; }

        public Command ToggleCommand => new Command((obj) =>
        {
            IsEnabled = !IsEnabled;
        });

        [JsonIgnore]
        public string BackColor { get { return IsEnabled ? "#50e3c2" : "#ffffff"; } }
        [JsonIgnore]
        public string BorderColor { get { return IsEnabled ? "#50e3c2" : "#9b9b9b"; } }
        [JsonIgnore]
        public string TextColor { get { return IsEnabled ? "#ffffff" : "#9b9b9b"; } }
    }
}
