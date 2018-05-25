using System;
using Xamarin.Forms;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CatalogueProgramModel
    {

		public CatalogueProgramModel(string TintColor)
		{
			Tint = TintColor;
		}

		public string Title { get; set; } = "10 min";

		public string SubTitle { get; set; } = "4 seances";    

		public bool IsEnabled { get; set; }

		public string Tint { get; set; } = "#ffffff";

		public string TextColor { get { return IsEnabled ? "#ffffff" : ("#9b9b9b"); } }

		public string BackColor { get { return IsEnabled ? Tint : "#ffffff"; } }

		public string BorderColor { get{ return IsEnabled ? Tint : "#d9d9d9"; } }

		public string Image { get { return IsEnabled ? "arrow.png" : "lock.png"; } }
    }
}
