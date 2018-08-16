using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using MeditSolution.PageModels;
using Rox;

namespace MeditSolution.Pages
{
	public partial class VideoPlayPage : BasePage
    {


        public VideoPlayPage()
        {
			Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

			InitializeComponent();

			On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

			videoView.PropertyChanged += VideoView_PropertyChanged;
        }

		void VideoView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName == VideoView.VideoStateProperty.PropertyName)
			{
			    if(videoView.VideoState == VideoStateType.Stopped)
				{
					(BindingContext as VideoPlayPageModel).CloseCommand.Execute(null);
				}
                else if(videoView.VideoState == VideoStateType.Playing)
                {
                    
                }
			}
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			videoView.Stop();  
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			videoView.PropertyChanged -= VideoView_PropertyChanged;
		}

		protected override bool OnBackButtonPressed()
        {
			videoView.Stop();
            return true;
        }
	}
}
