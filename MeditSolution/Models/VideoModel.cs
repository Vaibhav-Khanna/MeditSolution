using System;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class VideoModel
    {
		public VideoModel(Video video)
		{
			Video = video;
            
			Title = Settings.DeviceLanguage == "English" ? video.Name_EN : video.Name;
			Duration = video.Length;
		}

		public Video Video { get; set; }

		public string Title { get; set; }

		public string Duration { get; set; }

		public string Image { get; set; }
    }
}
