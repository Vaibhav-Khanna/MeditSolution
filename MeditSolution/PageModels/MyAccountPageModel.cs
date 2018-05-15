using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MyAccountPageModel : BasePageModel
    {
		public Command ModifyCommand => new Command(async () =>
       {
           await CoreMethods.PushPageModel<MyAccountModifyPageModel>();
       });
    }
}
