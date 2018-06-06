using System;
using MeditSolution.Models;
using Xamarin.Forms;
using MeditSolution.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MeditSolution.Helpers;
using MeditSolution.Resources;
using MeditSolution.Models.DataObjects;

namespace MeditSolution.PageModels
{
	public class CatalogueDetailPageModel : BasePageModel
    {
        
		TabMeditationModel TabMeditationModel { get; set; }
		public ObservableCollection<CatalogueProgramModel> Programs { get; set; }
		public string CoverPicture { get; set; }
		public string Header { get; set; }
		public string Description { get; set; }


		public async override void Init(object initData)
		{
			base.Init(initData);

			if (initData is TabMeditationModel)
			{
				TabMeditationModel = ((TabMeditationModel)initData);

				Header = TabMeditationModel.Title;
				Description = Settings.DeviceLanguage == "English" ? TabMeditationModel.Program.Description_EN : TabMeditationModel.Program.Description;
				CoverPicture = Constants.FileUrl + "files" + TabMeditationModel.Program.Cover;

				ChangeNavigationBackgroundColor(Color.FromHex(TabMeditationModel.Tint.Substring(1)));

				IsLoading = true;

				Programs = new ObservableCollection<CatalogueProgramModel>();

				var meditations = await StoreManager.MeditationStore.GetMeditationsByProgramId(TabMeditationModel.Program.Id);

				if (meditations != null)
					foreach (var item in meditations)
					{
						Programs.Add(new CatalogueProgramModel(TabMeditationModel, item));
					}
				else
					await ToastService.Show(AppResources.requestfailed);

				IsLoading = false;
			}
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			ChangeNavigationBackgroundColor(Color.FromHex(TabMeditationModel.Tint.Substring(1)));
		}

		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
			base.ViewIsDisappearing(sender, e);

			DefaultNavigationBackgroundColor();
		}

		public Command ProgramSelectCommand => new Command(async(obj) =>
		{
			
			var model = obj as CatalogueProgramModel;

			if (model.IsEnabled)
			{
				Dialog.ShowLoading();

				var user = StoreManager.UserStore.User;

				user.CurrentMeditationId = model.Meditation.Id;

				user.CurrentProgramId = model.Meditation.ProgramId;    

				await StoreManager.UserStore.UpdateCurrentUser(user);

				Dialog.HideLoading();

				await CoreMethods.PopPageModel();

				await CoreMethods.SwitchSelectedTab<MeditationTabPageModel>();
			}
			else
			{
				if(model.IsIncludedInSubscription)
				{
					var response = await CoreMethods.DisplayAlert(Settings.DeviceLanguage == "English" ? model.Meditation.Label_EN : model.Meditation.Label, AppResources.SubscribeMessage, AppResources.SubscribeNow, AppResources.NotNow);

                    if (response)
                    {
						await CoreMethods.PushPageModel<SubscriptionPageModel>();
						CoreMethods.RemoveFromNavigation<CatalogueDetailPageModel>(true);
                    }
				}
				else
				{
					if(StoreManager.UserStore.User.Subscription == Models.DataObjects.SubscriptionType.free)
					{
						var response = await CoreMethods.DisplayAlert(Settings.DeviceLanguage == "English" ? model.Meditation.Label_EN : model.Meditation.Label, AppResources.SubscribeMessage, AppResources.SubscribeNow, AppResources.NotNow);

                        if (response)
                        {
                            await CoreMethods.PushPageModel<SubscriptionPageModel>();
                            CoreMethods.RemoveFromNavigation<CatalogueDetailPageModel>(true);
                        }
					}
					else
					{
						//puchase the program
						Dialog.ShowLoading();

						await StoreManager.UserStore.UpdateCurrentUser(StoreManager.UserStore.User);

						var user = await StoreManager.ProgramStore.PayForNewProgram(model.Meditation.ProgramId,new PaidProgram(){ Id = model.Meditation.ProgramId, Ticket = "aaabbbbbbbbbbbbb", Platform = Device.RuntimePlatform.ToLower() });

						Dialog.HideLoading();
                                              
						if(user!=null)
						{
							await CoreMethods.DisplayAlert(AppResources.ProgramPaidThanks, "", AppResources.ok);
							await StoreManager.UserStore.UpdateCurrentUser(user);
							this.Init(TabMeditationModel);
						}
					}
				}
			}
		});

	}
}
