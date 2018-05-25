using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MeditationTabPageModel : BasePageModel
    {

		public bool IsEmpty { get; set; } = true;
		public ObservableCollection<object> Seances { get; set; } = new ObservableCollection<object>();
		public string Meditation { get; set; } = "Instant présent";
		public string MeditationDetail { get; set; } = "4 séances - 10 min";
		public string MeditationIcon { get; set; } = "pinstant.png";
		public string Tint { get; set; } = "#50e3c2";


		public override void Init(object initData)
		{
			base.Init(initData);

			Seances.Add(new SeancesModel(Tint) { Title = "Guidé", IsDownloaded = true, IsLocked = false, Model = this });
			Seances.Add(Tint); // connecting line
			Seances.Add(new SeancesModel(Tint) { Title = "Moins guidé", IsDownloaded = false, IsLocked = false, Model = this });
			Seances.Add(Tint); // connecting line
			Seances.Add(new SeancesModel(Tint) { Title = "Peu guidé", IsDownloaded = false, IsLocked = true, Model = this });
			Seances.Add(Tint); // connecting line
			Seances.Add(Tint); // connecting line
			Seances.Add(Tint); // connecting line
		}


		public Command StartCommand => new Command(() =>
		{
			IsEmpty = false;
		});

	}
}
