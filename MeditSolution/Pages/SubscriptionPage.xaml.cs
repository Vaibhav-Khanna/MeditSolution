using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using SkiaSharp;

using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class SubscriptionPage : BasePage
    {
        public SubscriptionPage()
        {
            InitializeComponent();

        }
       
		void Handle_PaintSurfaceL(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColor.Parse("50e3c2")
			};

			canvas.DrawCircle(info.Width / 2, info.Height / 2, info.Width / 2, paint);

			if(Device.RuntimePlatform==Device.iOS)
			canvasViewL.HeightRequest = info.Width/2;
			else
			{
				canvasViewL.HeightRequest = (info.Width / 3);
			}
		}

		void Handle_PaintSurfaceR(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColor.Parse("50e3c2")
			};

			canvas.DrawCircle(info.Width / 2, info.Height / 2, info.Width / 2, paint);
            
			if (Device.RuntimePlatform == Device.iOS)
                canvasViewR.HeightRequest = info.Width / 2;
            else
            {
				canvasViewR.HeightRequest = (info.Width / 3);
            }
		}

		void Handle_TappedL(object sender, System.EventArgs e)
		{
			canvasViewL.Opacity = 1;
			stackL.Opacity = 0.44;

			canvasViewR.Opacity = 0.24;
			stackR.Opacity = 0.44;

			imgL.IsVisible = true;
			imgR.IsVisible = false;

			(BindingContext as SubscriptionPageModel).IsMonthlySelected = true;
			(BindingContext as SubscriptionPageModel).OptionSelectedCommand.Execute(null);

			btSub.BackgroundColor = (Color)Application.Current.Resources["primary"];
			btSub.BorderColor = (Color)Application.Current.Resources["primary"];
			btSub.TextColor = (Color)Application.Current.Resources["white"];
		}

		void Handle_TappedR(object sender, System.EventArgs e)
        {
			canvasViewR.Opacity = 1;
            stackR.Opacity = 0.44;
            
            canvasViewL.Opacity = 0.24;
            stackL.Opacity = 0.44;

			imgL.IsVisible = false;
			imgR.IsVisible = true;

			(BindingContext as SubscriptionPageModel).IsMonthlySelected = false;
			(BindingContext as SubscriptionPageModel).OptionSelectedCommand.Execute(null);

			btSub.BackgroundColor = (Color)Application.Current.Resources["primary"];
            btSub.BorderColor = (Color)Application.Current.Resources["primary"];
            btSub.TextColor = (Color)Application.Current.Resources["white"];
        }

    }
}
