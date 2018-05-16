using System;
using System.Collections.Generic;
using MeditSolution.Models;
using MeditSolution.PageModels;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class PlusTabPage : BasePage
    {
        public PlusTabPage()
        {
            InitializeComponent();
        }

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			(BindingContext as PlusTabPageModel).MenuCommand.Execute((e.Item as MenuModel)?.Text);
			list.SelectedItem = null;
		}
    }
}
