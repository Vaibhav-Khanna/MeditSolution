using System;
using System.Collections.ObjectModel;
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
			await CoreMethods.PushPageModel<MeditationBreathePlayPageModel>(null, true);
		});

	}
}
