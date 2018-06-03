using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MeditSolution.Pages
{
	public partial class MeditationPlayPage : BasePage
	{
    
		public MeditationPlayPage()
		{
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
		}
        

	}
}
