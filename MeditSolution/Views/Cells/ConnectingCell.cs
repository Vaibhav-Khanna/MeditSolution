using System;

using Xamarin.Forms;

namespace MeditSolution.Views.Cells
{
    public class ConnectingCell : ContentView
    {
		BoxView box;

        public ConnectingCell()
        {

            HeightRequest = 20;

			box = new BoxView() { HorizontalOptions = LayoutOptions.Center, Color = (Color.FromHex("9b9b9b")), WidthRequest = 1, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = 20 };
			//box.SetBinding(BoxView.ColorProperty, ".");

            Content = box;
        }


		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if(BindingContext is string)
			{
				box.Color = Color.FromHex((BindingContext as string).Substring(1));
			}

		}
	}
}

