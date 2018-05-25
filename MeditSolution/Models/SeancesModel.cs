using System;
using MeditSolution.PageModels;
using Xamarin.Forms;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SeancesModel
    {
		public SeancesModel(string TintColor)
		{
			Tint = TintColor;
		}

		public MeditationTabPageModel Model { get; set; }

		private string Grey = "#9b9b9b";

		private string White = "#ffffff";

		public string Title { get; set; }

		public string Tint { get; set; } = "#50e3c2";
        
		public bool IsLocked { get; set; }

		public bool IsDownloaded { get; set; }

		public string BorderColor { get { return IsLocked ? Grey : Tint; } }

		public string FillColor { get { return IsLocked ? White : IsDownloaded ? White : Tint; } }

		public string TextColor { get { return IsLocked ? Grey : IsDownloaded ? Grey : White; } }
     
		public string IconTop { get { return IsLocked ? "locked.png" : IsDownloaded ? "playgreen.png" : "playwhite.png"; } }

		public string IconBottom { get { return IsLocked ? "cloud.png" : IsDownloaded ? "cloud.png" : "cloudcheck.png"; } }


		public Command PlayCommand => new Command(async() =>
		{
			await Model.CoreMethods.PushPageModel<SettingsPageModel>(data:true);
		});

		public Command DownloadCommand => new Command(() =>
        {
			IsDownloaded = !IsDownloaded;
        });
        
	}
}
