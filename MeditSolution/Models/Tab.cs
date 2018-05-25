using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using MeditSolution.PageModels;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Tab
    {
		public string TabName { get; set; }

		public CatalogueTabPageModel Model { get; set; }

		public int ColoumnCount { get; set; } = 2;

		public ObservableCollection<TabMeditationModel> Meditations { get; set; } = new ObservableCollection<TabMeditationModel>();
        
		public Command MeditationSelected => new Command((obj) =>
		{
			Model?.CoreMethods.PushPageModel<CatalogueDetailPageModel>(obj); 
		});
    }
}
