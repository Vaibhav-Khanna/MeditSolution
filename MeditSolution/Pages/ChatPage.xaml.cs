﻿using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
    public partial class ChatPage : BasePage
    {
        public ChatPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

		protected override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
