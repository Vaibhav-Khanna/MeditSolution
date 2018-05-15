using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class MeditationPlayPage : BasePage
    {
		bool isAnimating;

        public MeditationPlayPage()
        {
            InitializeComponent();
		}

		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			if (isAnimating)
				return;

			if ((BindingContext as MeditationPlayPageModel).IsPlaying)
				image.TranslationX = 0;
			else
				image.TranslationX = 8;

			isAnimating = true;
            
			image.ScaleTo(0.7, 150, Easing.CubicOut);
			await boxView.ScaleTo(0.7, 150, Easing.CubicOut);
            
			image.ScaleTo(1, 150, Easing.CubicOut);
			await boxView.ScaleTo(1, 150, Easing.CubicOut);

			isAnimating = false;
		}
       
    }
}
