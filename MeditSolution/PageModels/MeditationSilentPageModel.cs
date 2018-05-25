using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace MeditSolution.PageModels
{
	public class MeditationSilentPageModel : BasePageModel
    {
		object time;
		[PropertyChanged.DoNotNotify]
		public object SelectedTime { get { return time; } set { time = value;
				if(time!=null)	
					Time = $"{(time as ObservableCollection<object>).First()}:{(time as ObservableCollection<object>).Last()}"; } }

		public string Time { get; set; } = "1:00";


		public Command StartCommand => new Command(async() =>
		{
			await CoreMethods.PushPageModel<MeditationSilentPlayPageModel>(null, true);
		});
    } 
}
