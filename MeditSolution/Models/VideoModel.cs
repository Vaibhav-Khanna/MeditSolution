using System;
namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class VideoModel
    {
		public string Title { get; set; }

		public string Duration { get; set; }

		public string Image { get; set; }
    }
}
