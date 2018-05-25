using System;
using MeditSolution.PageModels;

namespace MeditSolution.Models
{
	public class ReminderModel : BasePageModel
	{
		TimeSpan _time;
		[PropertyChanged.DoNotNotify]
		public TimeSpan Time 
		{ get 
			{ return _time; }
			set 
			{ 
				_time = value;
				TimeString = _time.ToString("g");
				RaisePropertyChanged(); 
			} }

		public string TimeString { get; set; } = "00:00";

		public bool IsActive { get; set; } = true;

		public string Monday { get; set; } = "M";
		public string Tuesday { get; set; } = "T";
		public string Wednesday { get; set; } = "W";
		public string Thursday { get; set; } = "T";
		public string Friday { get; set; } = "F";
		public string Saturday { get; set; } = "S";
		public string Sunday { get; set; } = "S";
	}
}
