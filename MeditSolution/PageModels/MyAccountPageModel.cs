using System;
using Xamarin.Forms;
using MeditSolution.Resources;
using MeditSolution.Models.DataObjects;

namespace MeditSolution.PageModels
{
	public class MyAccountPageModel : BasePageModel
	{
		public string Evolution { get; set; }
		public string Email { get; set; }
		public string Icon { get; set; }
		public User user;

		public Command ModifyCommand => new Command(async () =>
	   {
		   await CoreMethods.PushPageModel<MyAccountModifyPageModel>(user);
	   });

		public override void Init(object initData)
		{
			base.Init(initData);

			UpdateUser();
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			UpdateUser();
		}

		void UpdateUser()
		{
			user = StoreManager.UserStore.User;

            Email = user.Email;
            Evolution = AppResources.evolution + " " + (user.CurrentLevel + 1);

            Icon = "level" + (user.CurrentLevel + 1) + ".json";
		}
	}
}
