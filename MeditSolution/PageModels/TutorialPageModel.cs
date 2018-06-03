using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
	public class TutorialPageModel : BasePageModel
	{
		public ObservableCollection<TutorialModel> ItemsSource { get; set; }

		public TutorialPageModel()
		{
			ItemsSource = new ObservableCollection<TutorialModel>();
			ItemsSource.Add(new TutorialModel() { Image = "logo.png", Text = "" });
			ItemsSource.Add(new TutorialModel() { Image = "vector1.png", Text = AppResources.tutorial1 });
			ItemsSource.Add(new TutorialModel() { Image = "vector2.png", Text = AppResources.tutorial2 });
			ItemsSource.Add(new TutorialModel() { Image = "vector3.png", Text = AppResources.tutorial3 });
			ItemsSource.Add(new TutorialModel() { Image = "vector4.png", Text = AppResources.tutorial4 });
		}
  
		public Command LoginCommand => new Command(async () =>
		{
			await CoreMethods.PushPageModel<LoginPageModel>(true, true, Device.RuntimePlatform == Device.iOS);
		});


		public Command SignUpCommand => new Command(async () =>
		{
			await CoreMethods.PushPageModel<LoginPageModel>(false, true, Device.RuntimePlatform == Device.iOS);
		});
	}
}
