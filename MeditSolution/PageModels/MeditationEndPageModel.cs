using System;
using MeditSolution.Resources;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MeditationEndPageModel : BasePageModel
    {
		public string ImageHeader { get { return IsMeditationEnd ? "unlock.png" : "stones.png"; } }
		public string ButtonNext { get { return IsMeditationEnd ? AppResources.medoverdetail : AppResources.nextseance; } }
		public string ButtonEnd { get { return IsMeditationEnd ? AppResources.explorecatalogue : AppResources.backtocatalogue; } }
		public string Header { get { return IsMeditationEnd ? AppResources.medover : AppResources.congrats; }}
		public string SubHeader { get { return IsMeditationEnd ? "" : AppResources.congratsdetail; } }
		public string TextColor { get { return IsMeditationEnd ? "#9b9b9b" : "#50E3C2"; } }

		public bool IsMeditationEnd;

		public string NextMeditation { get { return IsMeditationEnd ? AppResources.medoverdetail : AppResources.congratsdetail2; } }
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
