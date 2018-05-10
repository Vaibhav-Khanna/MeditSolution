using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditSolution.Pages
{
	public partial class CatalogueDetailPage : BasePage
    {
        public CatalogueDetailPage()
        {
            InitializeComponent();

			image.On<Xamarin.Forms.PlatformConfiguration.iOS>().UseBlurEffect(BlurEffectStyle.Light);
        }
    }
}
