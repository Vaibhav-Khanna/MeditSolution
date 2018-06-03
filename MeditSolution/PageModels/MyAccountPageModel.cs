using System;
using Xamarin.Forms;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
	public class MyAccountPageModel : BasePageModel
    {
		public string Evolution { get; set; }
		public string Email { get; set; }
		public string Icon { get; set; }

		public Command ModifyCommand => new Command(async () =>
       {
           await CoreMethods.PushPageModel<MyAccountModifyPageModel>();
       });

		public override void Init(object initData)
		{
			base.Init(initData);

			var user = StoreManager.UserStore.User;
            
			Email = user.Email;
			Evolution = AppResources.evolution + " " + (user.CurrentLevel + 1);

			if (Device.RuntimePlatform == Device.iOS)
				Icon = "level" + (user.CurrentLevel + 1) + ".png";
			else
				Icon = "level_" + (user.CurrentLevel + 1) + ".png";
		}
	}
}
