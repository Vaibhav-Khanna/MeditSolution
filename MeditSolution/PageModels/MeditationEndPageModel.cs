using System;
using MeditSolution.Resources;
using Xamarin.Forms;
using MeditSolution.Helpers;
using System.Threading.Tasks;

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

        //public string Icon { get { return "lock.json"; } }

		public bool IsMeditationEnd;

		public string NextMeditation { get { return IsMeditationEnd ? AppResources.medoverdetail : AppResources.congratsdetail2; } }
		public string NextMeditatonName { get; set; } 
		public string NextMeditatonDetail { get; set; } 

		string meditationID { get; set; }
		string programId { get; set; }

		public async override void Init(object initData)
		{
			base.Init(initData);

			if (initData is bool)
			{
                if (IsLoading)
                    return;

				IsLoading = true;

				var next = await StoreManager.MeditationStore.GetNextMeditation();

				IsLoading = false;

				if (next != null)
				{
                    // meditation completed
                    if (next.otherMeditation != null)
                    {
                        IsMeditationEnd = true;
                        NextMeditatonName = Settings.DeviceLanguage == "English" ? next.otherMeditation.label_en : next.otherMeditation.label;
                        NextMeditatonDetail = (next.otherMeditation.length / 60) + " min";
                        meditationID = next.otherMeditation._id;
                        programId = next.otherMeditation.programId;
                    }
					else if (next.levelUp != null) // session completed
					{
						IsMeditationEnd = false;
						NextMeditatonName = Settings.DeviceLanguage == "English" ? next.levelUp.label_en : next.levelUp.label;
						NextMeditatonDetail = (next.levelUp.length / 60) + " min";
						meditationID = next.levelUp._id;
						programId = next.levelUp.programId;
					}
					else
                        await CoreMethods.PopPageModel(null,modal:true);
				}
				else
                    await CoreMethods.PopPageModel(null, modal: true);
			}
		}
        

		public Command NextCommand => new Command(async() =>
		{
			if(!string.IsNullOrEmpty(meditationID) && !string.IsNullOrEmpty(programId))
			{
				Dialog.ShowLoading();

				var user = StoreManager.UserStore.User;

				user.CurrentMeditationId = meditationID;
				user.CurrentProgramId = programId;

				await StoreManager.UserStore.UpdateCurrentUser(user);

				Dialog.HideLoading();

				await CoreMethods.PopPageModel(true);
			}
		});

		public Command EndCommand => new Command(async() =>
        {
			await CoreMethods.PopPageModel(true);
			await CoreMethods.SwitchSelectedTab<CatalogueTabPageModel>();
        });
    }
}
