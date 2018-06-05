using System;
using MeditSolution.Models;
using Xamarin.Forms;
using MeditSolution.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MeditSolution.Helpers;
using MeditSolution.Resources;

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

		public Command ProgramCommand => new Command((obj) =>
		{
			var model = obj as CatalogueProgramModel;

			//model.IsEnabled = !model.IsEnabled;
		});

	}
}
