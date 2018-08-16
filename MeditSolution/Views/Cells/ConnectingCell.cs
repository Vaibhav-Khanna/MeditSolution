using System;

using Xamarin.Forms;

namespace MeditSolution.Views.Cells
{
    public class ConnectingCell : ViewCell
    {
		BoxView box;

        public ConnectingCell()
        {
            Height = 20;

			box = new BoxView() { HorizontalOptions = LayoutOptions.Center, Color = (Color.FromHex("9b9b9b")), WidthRequest = 1, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = 20 };

            View = box;
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

