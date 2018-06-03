using System;
using MeditSolution.Service;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.Helpers
{
    public class LanguageService
    {
		public void SetLanguage()
		{
			var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

			AppResources.Culture = ci;

			Settings.DeviceLanguage = ci.DisplayName;
           
			DependencyService.Get<ILocalize>().SetLocale(ci);
		}
    }
}
