using System;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;
using MeditSolution.Resources;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class TabMeditationModel
    {
		public TabMeditationModel(Program program)
		{
			Program = program;

			Title = Settings.DeviceLanguage == "English" ? program.Label_EN : Program.Label;

			SubTitle = program.TotalMeditations + " " + AppResources.programmes;

			if (!string.IsNullOrEmpty(program.Color) && program.Color.Contains("#"))
				Tint = program.Color;

			if(!string.IsNullOrEmpty(program.Icon))
			{
				Image = program.Icon;
			}
		}

		public Program Program { get; set; }

		public string Image { get; set; } = "pinitiation.png";

		public string Title { get; set; }

		public string Tint { get; set; } = "#ebb967";

		public string SubTitle { get; set; }
    }
}
