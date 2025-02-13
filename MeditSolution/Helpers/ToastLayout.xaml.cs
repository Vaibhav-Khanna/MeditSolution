﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using MeditSolution.Controls;

namespace MeditSolution.Helpers
{
    public partial class ToastLayout : PopupPage
    {
       
        public ToastLayout(string text,Color? color)
        {
            InitializeComponent();
            ToastLabel.Text = text;

            if(color != null)
            {
                container.BackgroundColor = (Color)color;
            }
        }

        protected override void OnAppearingAnimationEnd()
        {
            Device.StartTimer(new TimeSpan(0, 0, 4), () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Rg.Plugins.Popup.Services.PopupNavigation.PopAllAsync();
                });

                return false;
            });

            base.OnAppearingAnimationEnd();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Navigation.NavigationStack.Count == 1 && Navigation.ModalStack.Count == 0)
            {
                if(Navigation.NavigationStack[0].GetType() == typeof(CustomNavigation))
                {
                    Navigation.NavigationStack[0].SendBackButtonPressed();
                    return true;
                }
            }          
         
            return base.OnBackButtonPressed();
        }
    }
}
