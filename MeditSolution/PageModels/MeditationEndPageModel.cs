using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MeditationEndPageModel : BasePageModel
    {
		public string ImageHeader { get { return IsMeditationEnd ? "unlock.png" : "stones.png"; } }
		public string ButtonNext { get { return IsMeditationEnd ? "Étape suivante" : "Séance suivante"; } }
		public string ButtonEnd { get { return IsMeditationEnd ? "Explorer le catalogue" : "Retour au catalogue"; } }
		public string Header { get { return IsMeditationEnd ? "Étape terminée !" : "Féllicitation !"; }}
		public string SubHeader { get { return IsMeditationEnd ? "" : "Vous avez terminé votre séance"; } }
		public string TextColor { get { return IsMeditationEnd ? "#9b9b9b" : "#50E3C2"; } }

		public bool IsMeditationEnd;

		public string NextMeditation { get { return IsMeditationEnd ? "Étape suivante" : "Méditation suivante"; } }
		public string NextMeditatonName { get; set; } = "Instant présent !";
		public string NextMeditatonDetail { get; set; } = "15 min";

		public override void Init(object initData)
		{
			base.Init(initData);

			if (initData is bool)
			{
				IsMeditationEnd = ((bool)initData);

				if (IsMeditationEnd)
				{

				}
				else
				{

				}
			}
		}

		public Command NextCommand => new Command(() =>
		{

		});

		public Command EndCommand => new Command(async() =>
        {
			await CoreMethods.PopPageModel(true);
        });
    }
}
