using System;
namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class MenuModel
    {
		public string Text { get; set; }

		public string Image { get; set; }
    }
}
