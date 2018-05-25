using System;
using System.Collections.Generic;
using MeditSolution.Models.Abstract;
using SkiaSharp;
using Xamarin.Forms;

namespace MeditSolution.Views
{
	public partial class LanguageSettingView : ContentView
    {
		ILanguageSetting model;

        public LanguageSettingView()
        {
            InitializeComponent(); 
        }

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if((BindingContext as ILanguageSetting) != null)
			{
				model = (BindingContext as ILanguageSetting);

				Model_PropertyChanged(null, new System.ComponentModel.PropertyChangedEventArgs("DefaultLanguageEnglish"));
				Model_PropertyChanged(null, new System.ComponentModel.PropertyChangedEventArgs("DefaultGenderMan"));

				model.PropertyChanged -= Model_PropertyChanged;
				model.PropertyChanged += Model_PropertyChanged;
			}
			else
			{
				model.PropertyChanged -= Model_PropertyChanged;
			}
		}
       
		void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "DefaultLanguageEnglish")
			{
				if(model.DefaultLanguageEnglish!=null)
				{
					if(model.DefaultLanguageEnglish.Value)
					{
						uk.Source = "checkcta.png";
						fr.Source = "fr.png";
					}
					else
					{
						fr.Source = "checkcta.png";
						uk.Source = "uk.png";
					}
				}          
			}

			if(e.PropertyName == "DefaultGenderMan")
			{
				if(model.DefaultGenderMan!=null)
				{
					if(model.DefaultGenderMan.Value)
					{
						homme.Source = "checkcta.png";
						femme.Source = "femme.png";
					}
					else
					{
						femme.Source = "checkcta.png";
						homme.Source = "homme.png";
					}
				}
			}


			if (model.DefaultGenderMan != null && model.DefaultLanguageEnglish!=null)
			{
				btSave.IsVisible = true;
			}
			else
			{
				btSave.IsVisible = false;
			}

			if(model.IsPresenterPage)
			{
				lb_bottom.IsVisible = !btSave.IsVisible;	
			}
			else
			{
				lb_bottom.IsVisible = false;
			}

		}

		void Handle_FRTapped(object sender, System.EventArgs e)
        {
			model.DefaultLanguageEnglish = false; 
        }
        
		void Handle_UKTapped(object sender, System.EventArgs e)
        {
			model.DefaultLanguageEnglish = true; 
        }

		void Handle_ManTapped(object sender, System.EventArgs e)
        {
			model.DefaultGenderMan = true; 
        }

		void Handle_GirlTapped(object sender, System.EventArgs e)
        {
			model.DefaultGenderMan = false; 
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
                PathEffect = SKPathEffect.CreateDash(new float[] { 10, 10, 10, 10 }, 10)
            };

            SKRect skRectangle = new SKRect();
            skRectangle.Size = new SKSize(info.Width - 2, info.Height - 2);
            skRectangle.Location = new SKPoint(1, 1);


            canvas.DrawRect(skRectangle, paint);
        }
    }
}
