using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class MyAccountModifyPageModel : BasePageModel
    {
		public Command SaveCommand => new Command(async () =>
        {
			await CoreMethods.PopPageModel();
        });
    }
}
