using System;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class AvatarGrowPageModel : BasePageModel
    {
		
		public Command CloseCommand => new Command(async() =>
		{
			await CoreMethods.PopPageModel(true);
		});
    }
}
