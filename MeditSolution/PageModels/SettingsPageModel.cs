using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models.Abstract;
using MeditSolution.Helpers;
using MeditSolution.Models;

namespace MeditSolution.PageModels
{
	public class SettingsPageModel : BasePageModel, ILanguageSetting
    {
		
		public bool? DefaultLanguageEnglish { get; set; }
		public bool? DefaultGenderMan { get; set; }
		public bool IsPresenterPage { get; set; } = false;
		public string Title { get; set; } = AppResources.settings;

		SeancesModel Model;

		public override void Init(object initData)
		{
			base.Init(initData);
            
			if(initData is SeancesModel)
			{
				IsPresenterPage = true;
				Title = AppResources.yoursetting;
				Model = ((SeancesModel)initData);
			}
			else if(initData is bool)
			{
				IsPresenterPage = (bool)initData;

				if (IsPresenterPage)
					Title = AppResources.yoursetting;
				else
				{
					Title = AppResources.settings;

					if (!string.IsNullOrEmpty(Settings.Language) && !string.IsNullOrEmpty(Settings.Voice))
					{
						DefaultLanguageEnglish = Settings.Language == "en" ? true : false;
						DefaultGenderMan = Settings.Voice == "male" ? true : false;
					}
				}
			}   
		}

		public Command SaveCommand => new Command(async() =>
		{
			Settings.Language = DefaultLanguageEnglish.Value ? "en" : "fr";
            Settings.Voice = DefaultGenderMan.Value ? "male" : "female";

			if (IsPresenterPage)
			{	
				await CoreMethods.PushPageModel<MeditationPlayPageModel>(Model,animate: false);
				CoreMethods.RemoveFromNavigation<SettingsPageModel>();
			}
			else
			{
				await CoreMethods.PopPageModel();
			}
		});
	}
}
