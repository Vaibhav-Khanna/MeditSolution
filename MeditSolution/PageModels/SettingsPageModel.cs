using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models.Abstract;

namespace MeditSolution.PageModels
{
	public class SettingsPageModel : BasePageModel, ILanguageSetting
    {
		
		public bool? DefaultLanguageEnglish { get; set; }
		public bool? DefaultGenderMan { get; set; }
		public bool IsPresenterPage { get; set; } = false;
		public string Title { get; set; } = AppResources.settings;


		public override void Init(object initData)
		{
			base.Init(initData);
            
			if(initData is bool)
			{
				IsPresenterPage = (bool)initData;

				if (IsPresenterPage)
					Title = AppResources.yoursetting;
				else
				{
					Title = AppResources.settings;
					DefaultLanguageEnglish = false;
					DefaultGenderMan = false;
				}
			}
           
		}

		public Command SaveCommand => new Command(async() =>
		{
			if (IsPresenterPage)
			{
				await CoreMethods.PushPageModel<MeditationPlayPageModel>();
				CoreMethods.RemoveFromNavigation<SettingsPageModel>();
			}
			else
			await CoreMethods.PopPageModel();
		});
	}
}
