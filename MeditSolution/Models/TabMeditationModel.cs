using System;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class TabMeditationModel
    {
		public string Image { get; set; }

		public string Title { get; set; }

		public string Tint { get; set; } = "#000000";

		public string SubTitle { get; set; }
    }
}
