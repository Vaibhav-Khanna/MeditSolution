using System;

using Xamarin.Forms;

namespace MeditSolution.Views.Cells
{
	public class ConnectingCell : ViewCell
    {
        public ConnectingCell()
        {
         
			var box = new BoxView() { HorizontalOptions = LayoutOptions.Center, WidthRequest = 1, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = 20 };
			box.SetBinding(BoxView.ColorProperty, ".");

			View = box;
        }
    }
}

