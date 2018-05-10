using System;
using MeditSolution.Models;
using Xamarin.Forms;
using MeditSolution.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MeditSolution.PageModels
{
	public class CatalogueDetailPageModel : BasePageModel
    {
        
		TabMeditationModel TabMeditationModel { get; set; }
		public ObservableCollection<CatalogueProgramModel> Programs { get; set; }
		public string CoverPicture { get; set; }
		public string Header { get; set; }
		public string Description { get; set; }


		public override void Init(object initData)
		{
			base.Init(initData);

			if (initData is TabMeditationModel)
			{
				TabMeditationModel = ((TabMeditationModel)initData);

				if (Device.RuntimePlatform == Device.iOS)
				{
					Header = TabMeditationModel.Title;
					Description = TabMeditationModel.SubTitle;
					CoverPicture = TabMeditationModel.Image;
				}
                
				ChangeNavigationBackgroundColor(Color.FromHex(TabMeditationModel.Tint.Substring(1)));
    

				Programs = new ObservableCollection<CatalogueProgramModel>();

				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { IsEnabled = true });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });
				Programs.Add(new CatalogueProgramModel(TabMeditationModel.Tint) { });

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

			model.IsEnabled = !model.IsEnabled;
		});

	}
}
