using Xamarin.Forms;
using FreshMvvm;
using MeditSolution.PageModels;
using System;
using MeditSolution.Controls;
using DLToolkit.Forms.Controls;

namespace MeditSolution
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

			FlowListView.Init(); 

            var page = FreshPageModelResolver.ResolvePageModel<TutorialPageModel>();

            MainPage = new FreshNavigationContainer(page);
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Action<string> PostSuccessFacebookAction { get; set; }
    }
}
