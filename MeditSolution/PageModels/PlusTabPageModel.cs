using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class PlusTabPageModel : BasePageModel
    {
		public ObservableCollection<object> Menu { get; set; }

		public override void Init(object initData)
		{
			base.Init(initData);

			Menu = new ObservableCollection<object>();
            
			Menu.Add(new MenuModel(){ Text = "Mon compte", Image = "account.png" });
			Menu.Add(new MenuModel() { Text = "L’abonnement", Image = "plans.png" });
			Menu.Add("Méditer");

			Menu.Add(new MenuModel() { Text = "Méditation Silencieuse", Image = "meditationsilenceCopy.png" });
			Menu.Add(new MenuModel() { Text = "Cohérence cardiaque", Image = "coherencecardiaque.png" });
			Menu.Add(new MenuModel() { Text = "Rappels quotidiens", Image = "rappels.png" });
			Menu.Add(new MenuModel() { Text = "Vidéos", Image = "play.png" });
			Menu.Add(new MenuModel() { Text = "Paramètres", Image = "settings.png" });

			Menu.Add("");
			Menu.Add(new MenuModel() { Text = "CGU", Image = "" });
		}

		public Command MenuCommand => new Command(async(str) =>
		{
			switch (str)
			{
				case "Mon compte":
					await CoreMethods.PushPageModel<MyAccountPageModel>();
					break;
				case "Vidéos":
					await CoreMethods.PushPageModel<VideosPageModel>();
					break;
				case"Méditation Silencieuse":
					await CoreMethods.PushPageModel<MeditationSilentPageModel>();
					break;
				case "Cohérence cardiaque":
					await CoreMethods.PushPageModel<MeditationBreathePageModel>();
					break;
				default:
					break;
			}
		});

	}
}
