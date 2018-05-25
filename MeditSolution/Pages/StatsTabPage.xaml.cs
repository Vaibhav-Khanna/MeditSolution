using System;
using System.Collections.Generic;
using SkiaSharp;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class StatsTabPage : BasePage
    {
        public StatsTabPage()
        {
            InitializeComponent();
        }

		void Handle_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
				Color = SKColor.Parse("9b9b9b"),
                StrokeWidth = 1,
				StrokeCap = SKStrokeCap.Square,
				PathEffect = SKPathEffect.CreateDash(new float[]{ 10, 10, 10, 10 },10)
            };

			SKRect skRectangle = new SKRect();
			skRectangle.Size = new SKSize(info.Width-2, info.Height-2);
			skRectangle.Location = new SKPoint(1,1);
   

			canvas.DrawRect(skRectangle, paint);
		}
    }
}
