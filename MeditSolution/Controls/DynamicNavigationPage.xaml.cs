using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MeditSolution.Controls
{
	public partial class DynamicNavigationPage : NavigationPage
    {
		public DynamicNavigationPage(Page mainPage) : base(mainPage)
        {
            InitializeComponent();
        }
    }
}
