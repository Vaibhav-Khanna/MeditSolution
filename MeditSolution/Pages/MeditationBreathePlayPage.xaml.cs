using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MeditSolution.Pages
{
    public partial class MeditationBreathePlayPage : BasePage
    {
        bool isAnimating;
        MeditationBreathePlayPageModel Model;
        bool Stop = true;


        public MeditationBreathePlayPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as MeditationBreathePlayPageModel).CloseCommand.Execute(null);
            return true;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is MeditationBreathePlayPageModel)
            {
                Model = (((MeditationBreathePlayPageModel)BindingContext));
                Model.IsPlaying = false;
                PlayAnimation();
            }


            else
            {
                if (Model != null)
                    Model.Duration = 0;
            }
        }

        async void PlayAnimation()
        {
            if (!Stop)
            {
                await expand.ScaleTo(0.4, Convert.ToUInt32(Model.CycleDuration / 2) * 1000, Easing.CubicInOut);
               
            }
            else
            {
                await Task.Delay(200);
            }

            if (!Stop)
            {
                await expand.ScaleTo(1, Convert.ToUInt32(Model.CycleDuration / 2) * 1000, Easing.CubicInOut);
               
            }
            else
            {
                await Task.Delay(200);
            }

            if (Model.TotalSeconds.TotalSeconds != 0)
                PlayAnimation();
        }


        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (isAnimating)
                return;

            isAnimating = true;

            if (Model.IsPlaying)
            {
                image.TranslationX = 0;
                Stop = false;
                
            }
            else
            {
                image.TranslationX = 4;
                Stop = true;               
            }

            image.ScaleTo(0.7, 150, Easing.CubicOut);
            await boxView.ScaleTo(0.7, 150, Easing.CubicOut);

            image.ScaleTo(1, 150, Easing.CubicOut);
            await boxView.ScaleTo(1, 150, Easing.CubicOut);

            isAnimating = false;
        }
    }
}
