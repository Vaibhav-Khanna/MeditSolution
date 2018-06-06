using System;
using MeditSolution.PageModels;
using Xamarin.Forms;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;
using MeditSolution.Resources;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SeancesModel
    {
		public SeancesModel(Meditation meditation,int level)
		{
			Meditation = meditation;

			Level = level;   

			switch (level)
			{
				case 1 :
					Title = AppResources.Level1;
					break;
				case 2:
					Title = AppResources.Level2;
					break;
				case 3:
					Title = AppResources.Level3;
					break;
				case 4 :
					Title = AppResources.Level4;
					break;
				default:
					break;
			} 
		}

		public Meditation Meditation { get; set; }

		public MeditationTabPageModel Model { get; set; }

		public int Level { get; set; }

		private string Grey = "#9b9b9b";

		private string White = "#ffffff";

		public string Title { get; private set; }

		public string Tint { get; set; } = "#50e3c2";
        
		public bool IsLocked { get; set; }

		public bool IsDownloaded { get; set; }

		public string PlayTint { get { return IsDownloaded ? Tint : White; } }

		public string BorderColor { get { return IsLocked ? Grey : Tint; } }

		public string FillColor { get { return IsLocked ? White : IsDownloaded ? White : Tint; } }

		public string TextColor { get { return IsLocked ? Grey : IsDownloaded ? Grey : White; } }
     
		public string IconTop { get { return IsLocked ? "locked.png" : "playgreen.png"; } }

		public string IconBottom { get { return IsLocked ? "cloud.png" : IsDownloaded ? "cloud.png" : "cloudcheck.png"; } }
                

		public Command PlayCommand => new Command(async() =>
		{
			if(!IsLocked)
			{
				if (string.IsNullOrEmpty(Settings.Language))
					await Model.CoreMethods.PushPageModel<SettingsPageModel>(data: this);
				else
				{
					if (Level != 4)
						await Model.CoreMethods.PushPageModel<MeditationPlayPageModel>(this, animate: false);
					else
						await Model.CoreMethods.PushPageModel<MeditationSilentPlayPageModel>(this, modal: true);
				}
			}	
		});


		public Command DownloadCommand => new Command(() =>
        {
			
        });
        
	}
}
