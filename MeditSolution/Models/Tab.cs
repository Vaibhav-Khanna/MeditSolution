using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Tab
    {
		public string TabName { get; set; }

		public int ColoumnCount { get; set; } = 2;

		public ObservableCollection<TabMeditationModel> Meditations { get; set; } = new ObservableCollection<TabMeditationModel>();
        
		public Command MeditationSelected => new Command((obj) =>
		{

		});
    }
}
