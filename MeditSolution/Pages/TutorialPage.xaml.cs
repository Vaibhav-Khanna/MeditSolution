using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeditSolution.Pages
{
    public partial class TutorialPage : BasePage
    {
        public TutorialPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    //if (Navigation.NavigationStack.Count > 1)
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //        //return base.OnBackButtonPressed();
        //}
    }
}
